﻿/*
 Copyright (C) 2008-2009 Andrea Maggiulli
  
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

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QLNet;

namespace TestSuite
{

   [TestClass()]
   public class T_Quotes
   {
      double add10(double x) { return x + 10; }
      double mul10(double x) { return x * 10; }
      double sub10(double x) { return x - 10; }

      double add(double x, double y) { return x + y; }
      double mul(double x, double y) { return x * y; }
      double sub(double x, double y) { return x - y; }

      [TestMethod()]
      public void testObservable()
      {
         // Testing observability of quotes

         SimpleQuote me = new SimpleQuote(0.0);
         Flag f = new Flag();

         me.registerWith(f.update);
         me.setValue(3.14);

         if (!f.isUp())
            Assert.Fail("Observer was not notified of quote change");

      }
      [TestMethod()]
      public void testObservableHandle() 
      {

         // Testing observability of quote handles

         SimpleQuote me1 = new SimpleQuote(0.0);
         RelinkableHandle<Quote> h = new RelinkableHandle<Quote>(me1);

         Flag f = new Flag();

         h.registerWith(f.update);

         me1.setValue(3.14);
         
         if (!f.isUp())
           Assert.Fail("Observer was not notified of quote change");

         f.lower();
         SimpleQuote me2 = new SimpleQuote(0.0);
         h.linkTo(me2);

         if (!f.isUp())
           Assert.Fail("Observer was not notified of quote change");

      }

      [TestMethod()]
      public void testDerived() 
      {

         // Testing derived quotes

         Func<double, double>[] f = {add10,mul10,sub10};

         Quote me = new SimpleQuote(17.0);
         Handle<Quote> h = new Handle<Quote>(me);

         for (int i=0; i<3; i++) 
         {
           DerivedQuote derived = new DerivedQuote(h,f[i]);
           double x = derived.value(),
                  y = f[i](me.value());
           if (Math.Abs(x-y) > 1.0e-10)
               Assert.Fail("derived quote yields " + x + "function result is " + y);
         }
      
      }
   }
}
