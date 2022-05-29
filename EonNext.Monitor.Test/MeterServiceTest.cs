using Xunit;
using Moq;
using FluentAssertions;
using EonNext.Monitor.Core;
using System.Collections.Generic;
using System;

namespace EonNext.Monitor.Test

{

    public class MeterServiceTest
    {
        Mock<IMeterRepository> meterRepositoryMock;
        Mock<IDateTimeProvider> dateTimeProviderMock;
        IMeterService meterService;
        MeterServiceTest()
        {
            meterRepositoryMock = new Mock<IMeterRepository>();
            meterRepositoryMock.Setup(repo => repo.GetMeters()).Returns(() => new List<Meter> {
                new Meter {
                    Id = "TestMeter1",
                    Tariffs = {
                        new Tariff {
                            EnergyUnitPrice = 1000,
                            DailyBasePrice = 2500,
                            From = DateTime.Parse("2021/12/24 00:00"),
                            To = DateTime.Parse("2021/12/28 00:00"),
                            MeterId = "TestMeter1"
                        },
                        new Tariff {
                            EnergyUnitPrice = 1500,
                            DailyBasePrice = 3000,
                            From = DateTime.Parse("2021/12/28 00:00"),
                            MeterId = "TestMeter1"
                        }
                    },
                    Readings =  {
                        new Reading {
                            MeterId = "TestMeter1",
                            Type = ReadingType.Credit,
                            Value = 50 * 100 * 100,
                            Timestamp = DateTime.Parse("2021/12/24 00:00")
                        },
                        new Reading {
                            MeterId = "TestMeter1",
                            Type = ReadingType.Credit,
                            Value = 49 * 100 * 100,
                            Timestamp = DateTime.Parse("2021/12/25 00:00")
                        },
                        new Reading {
                            MeterId = "TestMeter1",
                            Type = ReadingType.Credit,
                            Value = 47 * 100 * 100,
                            Timestamp = DateTime.Parse("2021/12/26 00:00")
                        },
                        new Reading {
                            MeterId = "TestMeter1",
                            Type = ReadingType.Credit,
                            Value = 45 * 100 * 100,
                            Timestamp = DateTime.Parse("2021/12/28 00:00")
                        },
                        new Reading {
                            MeterId = "TestMeter1",
                            Type = ReadingType.Credit,
                            Value = 44 * 100 * 100,
                            Timestamp = DateTime.Parse("2021/12/29 00:00")
                        },
                        new Reading {
                            MeterId = "TestMeter1",
                            Type = ReadingType.Credit,
                            Value = 53 * 100 * 100,
                            Timestamp = DateTime.Parse("2021/12/30 00:00")
                        },
                    },
                    TopUps = {
                        new TopUp {
                            MeterId = "TestMeter1",
                            Amount = 50 * 100 * 100,
                            Timestamp =  DateTime.Parse("2021/12/24 00:00")
                        },
                        new TopUp {
                            MeterId = "TestMeter1",
                            Amount = 10 * 100 * 100,
                            Timestamp =  DateTime.Parse("2021/12/29 00:01")
                        }
                    }
                }
            });

            dateTimeProviderMock = new Mock<IDateTimeProvider>();
            dateTimeProviderMock.Setup(provider => provider.GetCurrentDate()).Returns(DateTime.Parse("2021/12/30 00:00"));

            IMeterService meterService = new MeterService()
            {
                MeterRepository = meterRepositoryMock.Object,
                DateTimeProvider = dateTimeProviderMock.Object
            };

        }

        [Fact]
        public void Should_ReturnValidConsumption_When_GetRemainingConsumption()
        {
            Consumption remainingConsumption = meterService.GetRemainingConsumption();

            remainingConsumption.Price.Should().Be(53 * 100 * 100);
            remainingConsumption.Energy.Should().Be(53 * 100 * 100 / 1500);
        }

        [Fact]
        public void Should_ReturnValidConsumption_When_GetRemainingConsumption_SpecificDate()
        {
            Consumption remainingConsumption = meterService.GetRemainingConsumption(DateTime.Parse("2021/12/26 00:00"));

            remainingConsumption.Price.Should().Be(47 * 100 * 100);
            remainingConsumption.Energy.Should().Be(47 * 100 * 100 / 1000);
        }

        [Fact]
        public void Should_ReturnValidConsumption_When_GetRemainingConsumption_SpecificDate_Estimate()
        {
            Consumption remainingConsumption = meterService.GetRemainingConsumption(DateTime.Parse("2021/12/27 00:00"));

            remainingConsumption.Price.Should().Be(46 * 100 * 100);
            remainingConsumption.Energy.Should().Be(46 * 100 * 100 / 1000);
        }

        [Fact]
        public void Should_ReturnValidConsumption_When_GetTotalConsumption()
        {
            Consumption totalConsumption = meterService.GetTotalConsumption();

            totalConsumption.Price.Should().Be(7 * 100 * 100);
            totalConsumption.Energy.Should().Be((5 * 100 * 100) / 1000 + (2 * 100 * 100) / 15000);
        }
        [Fact]
        public void Should_ReturnValidConsumption_When_GetAverageConsumption_Daily()
        {
            Consumption totalConsumption = meterService.GetAverageConsumption(interval: TimeSpan.FromDays(1));

            totalConsumption.Price.Should().Be(7 * 100 * 100 / 6);
            totalConsumption.Energy.Should().Be(((5 * 100 * 100) / 1000 + (2 * 100 * 100) / 15000) / 6);
        }
        [Fact]
        public void Should_ReturnValidConsumption_When_GetAverageConsumption_Hourly()
        {
            Consumption totalConsumption = meterService.GetAverageConsumption(interval: TimeSpan.FromHours(1));

            totalConsumption.Price.Should().Be(7 * 100 * 100 / (6 * 24));
            totalConsumption.Energy.Should().Be(((5 * 100 * 100) / 1000 + (2 * 100 * 100) / 15000) / (6 * 24));
        }
        [Fact]
        public void Should_ReturnValidConsumption_When_GetTotalConsumption_FromSpecificdDate()
        {
            Consumption totalConsumption = meterService.GetTotalConsumption(DateTime.Parse("2021/12/25 00:00"));

            totalConsumption.Price.Should().Be(6 * 100 * 100);
            totalConsumption.Energy.Should().Be((4 * 100 * 100) / 1000 + (2 * 100 * 100) / 15000);
        }
        [Fact]
        public void Should_ReturnValidConsumption_When_GetAverageConsumption_FromSpecificdDate_Daily()
        {
            Consumption totalConsumption = meterService.GetAverageConsumption(interval: TimeSpan.FromDays(1), from: DateTime.Parse("2021/12/25 00:00"));

            totalConsumption.Price.Should().Be(6 * 100 * 100 / 6);
            totalConsumption.Energy.Should().Be(((4 * 100 * 100) / 1000 + (2 * 100 * 100) / 15000) / 6);
        }
        [Fact]
        public void Should_ReturnValidConsumption_When_GetAverageConsumption_FromSpecificdDate_Hourly()
        {
            Consumption totalConsumption = meterService.GetAverageConsumption(interval: TimeSpan.FromHours(1), from: DateTime.Parse("2021/12/25 00:00"));

            totalConsumption.Price.Should().Be(6 * 100 * 100 / (6 * 24));
            totalConsumption.Energy.Should().Be(((4 * 100 * 100) / 1000 + (2 * 100 * 100) / 15000) / (6 * 24));
        }

        [Fact]
        public void Should_ReturnValidConsumption_When_GetTotalConsumption_FromSpecificDateToSpecificDate()
        {
            Consumption totalConsumption = meterService.GetTotalConsumption(DateTime.Parse("2021/12/26 00:00"), DateTime.Parse("2021/12/29 00:00"));

            totalConsumption.Price.Should().Be(3 * 100 * 100);
            totalConsumption.Energy.Should().Be((2 * 100 * 100) / 1000 + (1 * 100 * 100) / 15000);
        }
        [Fact]
        public void Should_ReturnValidConsumption_When_GetAverageConsumption_FromSpecificdDateToSpecifiedDate_Daily()
        {
            Consumption totalConsumption = meterService.GetAverageConsumption(interval: TimeSpan.FromDays(1), from: DateTime.Parse("2021/12/26 00:00"), to: DateTime.Parse("2021/12/29 00:00"));

            totalConsumption.Price.Should().Be(3 * 100 * 100 / 6);
            totalConsumption.Energy.Should().Be(((2 * 100 * 100) / 1000 + (1 * 100 * 100) / 15000) / 6);
        }
        [Fact]
        public void Should_ReturnValidConsumption_When_GetAverageConsumption_FromSpecificdDateToSpecifiedDate_Hourly()
        {
            Consumption totalConsumption = meterService.GetAverageConsumption(interval: TimeSpan.FromHours(1), from: DateTime.Parse("2021/12/26 00:00"), to: DateTime.Parse("2021/12/29 00:00"));

            totalConsumption.Price.Should().Be(3 * 100 * 100 / (6 * 24));
            totalConsumption.Energy.Should().Be(((2 * 100 * 100) / 1000 + (1 * 100 * 100) / 15000) / (6 * 24));
        }
    }
}