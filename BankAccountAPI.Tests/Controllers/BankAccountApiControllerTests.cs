using AutoMapper;
using BankAccountAPI.Controllers;
using BankAccountAPI.Models;
using BankAccountAPI.Models.Dto;
using BankAccountAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountAPI.Tests.Controllers
{
	public class BankAccountApiControllerTests
	{
		private readonly Mock<IBankAccountRepository> _mockRepository;
		private readonly Mock<IMapper> _mockMapper;
		private readonly Mock<ILogger<BankAccountApiController>> _mockLogger;
		private readonly BankAccountApiController _controller;

		public BankAccountApiControllerTests()
		{
			_mockRepository = new Mock<IBankAccountRepository>();
			_mockMapper = new Mock<IMapper>();
			_mockLogger = new Mock<ILogger<BankAccountApiController>>();

			_controller = new BankAccountApiController(_mockRepository.Object, _mockMapper.Object, _mockLogger.Object);
		}

		[Fact]
		public void Get_ReturnsAllBankAccounts()
		{
			// Arrange
			var bankAccounts = new List<BankAccount>
			{
				new BankAccount { BankAccountId = 1, BankAccountNumber = "12345678", BankAccountAmount = 1000 },
				new BankAccount { BankAccountId = 2, BankAccountNumber = "87654321", BankAccountAmount = 500 }
			};

			_mockRepository.Setup(repo => repo.GetAllAccounts()).Returns(bankAccounts);
			_mockMapper.Setup(mapper => mapper.Map<IEnumerable<BankAccountDto>>(bankAccounts))
				.Returns(new List<BankAccountDto>
				{
					new BankAccountDto { BankAccountId = 1, BankAccountNumber = "12345678", BankAccountAmount = 1000 },
					new BankAccountDto { BankAccountId = 2, BankAccountNumber = "87654321", BankAccountAmount = 500 }
				});

			// Act
			var result = _controller.Get();

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result.Result);
			var responseDto = Assert.IsType<ResponseDto>(okResult.Value);
			Assert.True(responseDto.IsSuccess);
			Assert.NotNull(responseDto.Result);
			var accountDtos = Assert.IsAssignableFrom<IEnumerable<BankAccountDto>>(responseDto.Result);
			Assert.Equal(2, accountDtos.ToList().Count);
		}

		[Fact]
		public void Get_ReturnsBankAccountById()
		{
			// Arrange
			int accountId = 1;
			var bankAccount = new BankAccount { BankAccountId = accountId, BankAccountNumber = "12345678", BankAccountAmount = 1000 };

			_mockRepository.Setup(repo => repo.GetAccountById(accountId)).Returns(bankAccount);
			_mockMapper.Setup(mapper => mapper.Map<BankAccountDto>(bankAccount))
				.Returns(new BankAccountDto { BankAccountId = accountId, BankAccountNumber = "12345678", BankAccountAmount = 1000 });

			// Act
			var result = _controller.Get(accountId);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result.Result);
			var responseDto = Assert.IsType<ResponseDto>(okResult.Value);
			Assert.True(responseDto.IsSuccess);
			Assert.NotNull(responseDto.Result);
			var accountDto = Assert.IsType<BankAccountDto>(responseDto.Result);
			Assert.Equal(accountId, accountDto.BankAccountId);
		}
	}
}
