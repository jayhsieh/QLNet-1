/*
 Copyright (C) 2008 Alessandro Duci
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

using System;
using System.Collections.Generic;
using System.Text;

namespace QLNet {
    //! Indian calendars
    /*! Holidays for the National Stock Exchange
        (data from <http://www.nse-india.com/>):
        <ul>
        <li>Saturdays</li>
        <li>Sundays</li>
        <li>Republic Day, January 26th</li>
        <li>Good Friday</li>
        <li>Ambedkar Jayanti, April 14th</li>
        <li>Independence Day, August 15th</li>
        <li>Gandhi Jayanti, October 2nd</li>
        <li>Christmas, December 25th</li>
        </ul>

        Other holidays for which no rule is given (data available for
        2005-2007 only:)
        <ul>
        <li>Bakri Id</li>
        <li>Moharram</li>
        <li>Mahashivratri</li>
        <li>Holi</li>
        <li>Ram Navami</li>
        <li>Mahavir Jayanti</li>
        <li>Id-E-Milad</li>
        <li>Maharashtra Day</li>
        <li>Buddha Pournima</li>
        <li>Ganesh Chaturthi</li>
        <li>Dasara</li>
        <li>Laxmi Puja</li>
        <li>Bhaubeej</li>
        <li>Ramzan Id</li>
        <li>Guru Nanak Jayanti</li>
        </ul>

        \ingroup calendars
    */
    public class India : Calendar {
        public India() : base(Impl.Singleton) { }

        class Impl : Calendar.WesternImpl {
            public static readonly Impl Singleton = new Impl();
            private Impl() { }
         
            public override string name() { return "National Stock Exchange of India"; }
            public override bool isBusinessDay(Date date) {
                DayOfWeek w = date.DayOfWeek;
                int d = date.Day, dd = date.DayOfYear;
                Month m = (Month)date.Month;
                int y = date.Year;
                int em = easterMonday(y);

                if (isWeekend(w)
                    // Republic Day
                    || (d == 26 && m == Month.January)
                    // Good Friday
                    || (dd == em-3)
                    // Ambedkar Jayanti
                    || (d == 14 && m == Month.April)
                    // Independence Day
                    || (d == 15 && m == Month.August)
                    // Gandhi Jayanti
                    || (d == 2 && m == Month.October)
                    // Christmas
                    || (d == 25 && m == Month.December)
                    )
                    return false;
                if (y == 2005) {
                    // Moharram, Holi, Maharashtra Day, and Ramzan Id fall
                    // on Saturday or Sunday in 2005
                    if (// Bakri Id
                        (d == 21 && m == Month.January)
                        // Ganesh Chaturthi
                        || (d == 7 && m == Month.September)
                        // Dasara
                        || (d == 12 && m == Month.October)
                        // Laxmi Puja
                        || (d == 1 && m == Month.November)
                        // Bhaubeej
                        || (d == 3 && m == Month.November)
                        // Guru Nanak Jayanti
                        || (d == 15 && m == Month.November)
                        )
                        return false;
                }
                if (y == 2006) {
                    if (// Bakri Id
                        (d == 11 && m == Month.January)
                        // Moharram
                        || (d == 9 && m == Month.February)
                        // Holi
                        || (d == 15 && m == Month.March)
                        // Ram Navami
                        || (d == 6 && m == Month.April)
                        // Mahavir Jayanti
                        || (d == 11 && m == Month.April)
                        // Maharashtra Day
                        || (d == 1 && m == Month.May)
                        // Bhaubeej
                        || (d == 24 && m == Month.October)
                        // Ramzan Id
                        || (d == 25 && m == Month.October)
                        )
                        return false;
                }
                if (y == 2007) {
                    if (// Bakri Id
                        (d == 1 && m == Month.January)
                        // Moharram
                        || (d == 30 && m == Month.January)
                        // Mahashivratri
                        || (d == 16 && m == Month.February)
                        // Ram Navami
                        || (d == 27 && m == Month.March)
                        // Maharashtra Day
                        || (d == 1 && m == Month.May)
                        // Buddha Pournima 
                        || (d == 2 && m == Month.May)
                        // Laxmi Puja
                        || (d == 9 && m == Month.November)
                        // Bakri Id (again)
                        || (d == 21 && m == Month.December)
                        )
                        return false;
                }
                return true;
            }
        }
    }
}




    