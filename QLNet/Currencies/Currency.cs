/*
 This file is part of QLNet Project http://trac2.assembla.com/QLNet
 
 QLNet is a porting of QuantLib, a free-software/open-source library
 for financial quantitative analysts and developers - http://quantlib.org/
 The license is available online at http://quantlib.org/license.shtml.
 
 This program is distributed in the hope that it will be useful, but WITHOUT
 ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
 FOR A PARTICULAR PURPOSE.  See the license for more details.
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace QLNet
{
   public class Currency
   {
      protected struct Data
      {
         public string name;
         public string code;
         public int numeric;
         public string symbol;
         public string fractionSymbol;
         public int fractionsPerUnit;
         public Rounding rounding;
         public Currency triangulated;
         public string formatString;

         
         public Data(string name, string code, int numericCode, string symbol, string fractionSymbol,
                     int fractionsPerUnit, Rounding rounding, string formatString)
            : this(name, code, numericCode, symbol, fractionSymbol, fractionsPerUnit, rounding, formatString, new Currency())
         {}

         public Data(string name, string code, int numericCode, string symbol, string fractionSymbol,
                     int fractionsPerUnit, Rounding rounding, string formatString, Currency triangulationCurrency)
         {
            this.name = name;
            this.code = code;
            this.numeric = numericCode;
            this.symbol = symbol;
            this.fractionSymbol = fractionSymbol;
            this.fractionsPerUnit = fractionsPerUnit;
            this.rounding = rounding;
            this.triangulated = triangulationCurrency ;
            this.formatString = formatString;

         }

      }
      
      protected Data _data;

      public Currency()
      {
      }

      #region Get/Set
      /// <summary>
      /// currency name, e.g, "U.S. Dollar"
      /// </summary>
      public string name
      {
         get { return _data.name ; }
      }
      /// <summary>
      /// ISO 4217 three-letter code, e.g, "USD"
      /// </summary>
      public string code
      {
         get { return _data.code; }
      }
      /// <summary>
      /// ISO 4217 numeric code, e.g, "840"
      /// </summary>
      public int numericCode
      {
         get { return _data.numeric; }
      }
      /// <summary>
      /// symbol, e.g, "$"
      /// </summary>
      public string Symbol
      {
         get { return _data.symbol; }
      }
      /// <summary>
      /// fraction symbol, e.g, "�"
      /// </summary>
      public string fractionSymbol
      {
         get { return _data.fractionSymbol; }
      }
      /// <summary>
      /// number of fractionary parts in a unit, e.g, 100
      /// </summary>
      public int fractionsPerUnit
      {
         get { return _data.fractionsPerUnit; }
      }
      /// <summary>
      /// rounding convention
      /// </summary>
      public Rounding rounding
      {
         get { return _data.rounding; }
      }
      /// <summary>
      /// output format
      /// The format will be fed three positional parameters,
      /// namely, value, code, and symbol, in this order.
      /// </summary>
      public string format
      {
         get { return _data.formatString; }
      }
      /// <summary>
      /// currency used for triangulated exchange when required
      /// </summary>
      public Currency triangulationCurrency
      {
         get { return _data.triangulated; }
      }
      #endregion

      #region Methods
      /// <summary>
      /// is this a usable instance?
      /// </summary>
      /// <returns></returns>
      public bool empty()
      {
         return (_data.name == string.Empty); 
      }

      public static bool operator ==(Currency c1, Currency c2)
      {
         return (c1.name == c2.name);
      }

      public static bool operator !=(Currency c1, Currency c2)
      {
         return !(c1.name == c2.name);
      }

      public override string ToString()
      {
         if (!this.empty())
            return this.code;
        else
            return "null currency";
      }

      #endregion


   }
}
