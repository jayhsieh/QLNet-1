/*
 Copyright (C) 2008 Toyin Akin (toyin_akin@hotmail.com)
 Copyright (C) 2008 Siarhei Novik (snovik@gmail.com)
  
 This file is part of QLNet Project http://www.qlnet.org

 QLNet is free software: you can redistribute it and/or modify it
 under the terms of the QLNet license.  You should have received a
 copy of the license along with this program; if not, license is  
 available online at <http://trac2.assembla.com/QLNet/wiki/License>.
  
 QLNet is a based on QuantLib, a free-software/open-source library
 for financial quantitative analysts and developers - http://quantlib.org/
 The QuantLib license is available online at http://quantlib.org/license.shtml.
 
 This program is distributed in the hope that it will be useful, but WITHOUT
 ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
 FOR A PARTICULAR PURPOSE.  See the license for more details.
*/

using QLNet.Currencies;
using QLNet.Time.DayCounters;

namespace QLNet
{
	/// <summary>
	/// GBP LIBOR rate
	/// 
	/// Pound Sterling LIBOR fixed by BBA.
	/// </summary>
	/// <remarks>
	/// See <http://www.bba.org.uk/bba/jsp/polopoly.jsp?d=225&a=1414>.
	/// </remarks>
	public class GBPLibor : Libor
	{
		public GBPLibor(Period tenor)
			: this(tenor, new Handle<YieldTermStructure>())
		{
		}

		public GBPLibor(Period tenor, Handle<YieldTermStructure> h)
			: base("GBPLibor", tenor, 0, new GBPCurrency(), new UnitedKingdom(UnitedKingdom.Market.Exchange), new Actual365Fixed(), h)
		{
		}
	}
}
