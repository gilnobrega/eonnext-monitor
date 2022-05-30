using Xunit;
using Moq;
using FluentAssertions;
using EonNext.Monitor.Core;
using System.Collections.Generic;
using System;

namespace EonNext.Monitor.Test
{
    public class EonNextClientTest
    {
        Mock<IMeterRepository> meterRepositoryMock;
        Mock<IReadingRepository> readingRepositoryMock;

        EonNextClientTest()
        {

        }
    }
}