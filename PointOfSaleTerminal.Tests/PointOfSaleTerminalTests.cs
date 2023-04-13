using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace PointOfSaleTerminal.Tests
{
	[TestFixture]
	public class PointOfSaleTerminalTests
	{
		List<Pricing> _pricingList = new List<Pricing>
		{
			new Pricing { Code = "A", UnitPrice = 1.25m, VolumeQuantity = 3, VolumePrice = 3.00m },
			new Pricing { Code = "B", UnitPrice = 4.25m },
			new Pricing { Code = "C", UnitPrice = 1.00m, VolumeQuantity = 6, VolumePrice = 5.00m },
			new Pricing { Code = "D", UnitPrice = 0.75m },
		};

		PointOfSaleTerminal _terminal;
		
		[SetUp]
		public void Setup()
		{
			_terminal = new PointOfSaleTerminal();
			_terminal.SetPricing(_pricingList);
		}

		[Test]
		public void TestMixedCartWithVolumePriceOfA()
		{
			_terminal.ScanProduct("A");
			_terminal.ScanProduct("B");
			_terminal.ScanProduct("C");
			_terminal.ScanProduct("D");
			_terminal.ScanProduct("A");
			_terminal.ScanProduct("B");
			_terminal.ScanProduct("A");

			Assert.AreEqual(13.25, _terminal.CalculateTotal());
		}

		[Test]
		public void TestVolumePricePlusUnitPriceOfC()
		{
			_terminal.ScanProduct("C");
			_terminal.ScanProduct("C");
			_terminal.ScanProduct("C");
			_terminal.ScanProduct("C");
			_terminal.ScanProduct("C");
			_terminal.ScanProduct("C");
			_terminal.ScanProduct("C");

			Assert.AreEqual(6.00, _terminal.CalculateTotal());
		}

		[Test]
		public void TestOneOfEach()
		{
			_terminal.ScanProduct("A");
			_terminal.ScanProduct("B");
			_terminal.ScanProduct("C");
			_terminal.ScanProduct("D");

			Assert.AreEqual(7.25, _terminal.CalculateTotal());
		}

		[Test]
		public void TestUknownProductCode()
		{
			Assert.Throws<ArgumentException>(() => _terminal.ScanProduct("E"));		
		}

		[Test]
		public void TestEmptyCart()
		{
			//Test library doesn't unexpectedly break on empty cart.
			Assert.AreEqual(0.00, _terminal.CalculateTotal());
		}

		[Test]
		public void TestNoPricingSetAndNoScannedProductDoesNotThrow()
		{
			_terminal = new PointOfSaleTerminal(); //undo the setup

			Assert.AreEqual(0, _terminal.CalculateTotal());
		}

		[Test]
		public void TestNoPricingSetAndScannedProductDoesThrow()
		{
			_terminal = new PointOfSaleTerminal(); //undo the setup

			Assert.Throws<ArgumentException>(() => _terminal.ScanProduct("E"));
		}
	}
}
