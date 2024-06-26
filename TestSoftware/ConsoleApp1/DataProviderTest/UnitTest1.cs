using AutoMapper;
using ConsoleApp1.Application;
using ConsoleApp1.Application.Interfaces;
using ConsoleApp1.CrossCuting;
using ConsoleApp1.Models;
using ConsoleApp1.Repository.Interfaces;
using Moq;
using Serilog;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DataProviderTest
{
    public class Tests
    {
        private readonly Mock<IIsinRepository> _isinRepository;
        private readonly Mock<ISecuritiesDataProviderService> _securitiesDataProviderService;
        private readonly Mock<ILogger> _logger;
        private readonly Mock<IMapper> _mapper;
        private readonly IIsinService _isinService;

        public Tests(Mock<IIsinRepository> isinRepository,
            Mock<ISecuritiesDataProviderService> securitiesDataProviderService
            , Mock<ILogger> logger,
            Mock<IMapper> mapper)
        {
            _isinRepository = isinRepository;
            _securitiesDataProviderService = securitiesDataProviderService;
            _logger = logger;
            _mapper = mapper;
            _isinService = new IsinService(_isinRepository.Object, _logger.Object, _mapper.Object, _securitiesDataProviderService.Object);
        }

        [Fact]
        public async Task RequestIsinString_shoud_validateResponse_SuccesAsync()
        {
            // setup
            var response = new DataProviderIsinResponse() { DataCreateOfRequest = DateTime.Now, IsinNumber = "SlQZJ97sCk6T", RequestedList = null };
            _securitiesDataProviderService.Setup(x => x.GetIsinAsync(It.IsAny<string>())).ReturnsAsync(response);
            _mapper.Setup(x => x.Map<IsinModel>(It.IsAny<DataProviderIsinResponse>())).Returns(new IsinModel() { IsinNumber = response.IsinNumber, DataCreate = response.DataCreateOfRequest });
            _mapper.Setup(x => x.Map<IsinResponse>(It.IsAny<DataProviderIsinResponse>())).Returns(new IsinResponse() { IsinNumber = response.IsinNumber, DataCreate = response.DataCreateOfRequest });
            _isinRepository.Setup(x => x.Insert(It.IsAny<IsinModel>())).Returns(new InsertResponse());

            // Action
            var result = await _isinService.GetSecururityFinancialInstrument("SlQZJ97sCk6T");


            //Acssert
            Assert.NotNull(result);
            Assert.Equal(response.IsinNumber, result!.IsinNumber);
            Assert.Equal(response.DataCreateOfRequest, result!.DataCreate);
            Assert.Equal(response.RequestedList, result!.RequestedList);
            _logger.Verify(x => x.Information(It.IsAny<string>()), Times.Exactly(2));

        }

        [Fact]
        public async Task RequestIsinString_shoud_validateResponse_FaillExeptiion()
        {
            // setup
            var response = new DataProviderIsinResponse() { DataCreateOfRequest = DateTime.Now, IsinNumber = "SlQZJ97sCk6T", RequestedList = null };
            _securitiesDataProviderService.Setup(x => x.GetIsinAsync(It.IsAny<string>())).Throws(new Exception("TimeOut"));
            _mapper.Setup(x => x.Map<IsinModel>(It.IsAny<DataProviderIsinResponse>())).Returns(new IsinModel() { IsinNumber = response.IsinNumber, DataCreate = response.DataCreateOfRequest });
            _mapper.Setup(x => x.Map<IsinResponse>(It.IsAny<DataProviderIsinResponse>())).Returns(new IsinResponse() { IsinNumber = response.IsinNumber, DataCreate = response.DataCreateOfRequest });
            _isinRepository.Setup(x => x.Insert(It.IsAny<IsinModel>())).Returns(new InsertResponse());




            //Acssert            
            await Assert.ThrowsAsync<Exception>(() => _isinService.GetSecururityFinancialInstrument("SlQZJ97sCk6T"));
            _logger.Verify(x => x.Information(It.IsAny<string>()), Times.Once);
            _logger.Verify(x => x.Error(It.IsAny<string>()), Times.Once);


        }
    }
}