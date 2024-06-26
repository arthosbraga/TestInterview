using AutoMapper;
using ConsoleApp1.Application.Interfaces;
using ConsoleApp1.CrossCuting;
using ConsoleApp1.Models;
using ConsoleApp1.Repository.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1.Application
{
    public class IsinService : IIsinService
    {
        private readonly IIsinRepository _isinRepository;
        private readonly ISecuritiesDataProviderService _securitiesDataProviderService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public IsinService(IIsinRepository isinRepository,
            ILogger ilogger,
            IMapper mapper,
            ISecuritiesDataProviderService securitiesDataProviderService)
        {
            _isinRepository = isinRepository;
            _logger = ilogger;
            _mapper = mapper;
            _securitiesDataProviderService = securitiesDataProviderService;
        }

        public async Task<List<IsinResponse>> GetBatchSecururityFinancialInstrument(List<string>? isinList)
        {
            List<IsinResponse> isinResponses = new();

            if (isinList != null && isinList.Any())
            {

                await Parallel.ForEachAsync(isinList, async (isin, cancel) =>
                {
                    try
                    {
                        var isinresponse = await GetSecururityFinancialInstrument(isin);
                        if (isinresponse != null)
                        {
                            isinResponses.Add(isinresponse);
                        }
                        else
                        {
                            _logger.Information($"isin dont proceced {isin}");
                        }

                    }
                    catch (Exception e)
                    {

                        _logger.Warning($"Nao o correubem {isin}");
                    }
                });
            }
            else
            {
                throw new Exception("List null or Empty");
            }
            return isinResponses;
        }

        public async Task<IsinResponse?> GetSecururityFinancialInstrument(string? isin)
        {
            try
            {
                _logger.Information("Iniciate process");

                if (ValidateIsin(isin))
                {

                    var response = await _securitiesDataProviderService.GetIsinAsync(isin);

                    var isinPersit = _mapper.Map<IsinModel>(response);


                    var result = _isinRepository.Insert(isinPersit);


                    _logger.Information("Finishing request");

                    return _mapper.Map<IsinResponse>(response);
                }
            }
            catch (System.Exception e)
            {
                _logger.Error(e.Message);
                throw;
            }
            return null;
        }

        private bool ValidateIsin(string? isin)
        {
            return isin != null && isin.Length == 12;
        }
    }
}
