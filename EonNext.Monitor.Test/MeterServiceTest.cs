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
        Mock<IMeterRepository> readingMock;

        MeterServiceTest()
        {
            readingMock = new Mock<IMeterRepository>();
            readingMock.Setup(repo => repo.GetMeters()).Returns(() => new List<Meter> {
                new Meter {
                    Id = "TestMeter1",
                    Tariffs = {
                        new Tariff {
                            EnergyUnitPrice = 1223,
                            DailyBasePrice = 56,
                            From = DateTime.Parse("2021/12/24"),
                            MeterId = "TestMeter1"
                        }
                    },
                    Readings =  {
                        new Reading {
                            MeterId = "TestMeter1",
                            Type = ReadingType.Credit,
                            Value = 12345,
                            Timestamp = DateTime.Parse("2021/12/24")
                        },
                        new Reading {
                            MeterId = "TestMeter1",
                            Type = ReadingType.Credit,
                            Value = 12169,
                            Timestamp = DateTime.Parse("2021/12/25")
                        },
                        new Reading {
                            MeterId = "TestMeter1",
                            Type = ReadingType.Credit,
                            Value = 11739,
                            Timestamp = DateTime.Parse("2021/12/26")
                        },
                    },
                    TopUps = {
                        new TopUp {
                            MeterId = "TestMeter1",
                            Amount = 12345,
                            Timestamp =  DateTime.Parse("2021/12/24")
                        }
                    }
                }
            });
        }
        [Fact]
        public void Should_ReturnValidConsumption_When_GetAverageDailyConsumption()
        {

        }
    }
}