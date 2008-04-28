/*
 Copyright (C) 2008 Toyin Akin (toyin_akin@hotmail.com)
  
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
using System.Linq;
using System.Text;

namespace QLNet {

	//! Digital option replication strategy
//    ! Specification of replication strategies used to price
//        the embedded digital option in a digital coupon.
//    
	public struct Replication
	{
		public enum Type: int
		{
			Sub,
			Central,
			Super
		}
	}

	public class DigitalReplication
	{
        private double gap_;
        private Replication.Type replicationType_;

		public DigitalReplication(Replication.Type t) : this(t, 1e-4)
		{
		}
		public DigitalReplication() : this(Replication.Type.Central, 1e-4)
		{
		}
		public DigitalReplication(Replication.Type t, double gap)
		{
			gap_ = gap;
			replicationType_ = t;
		}

        public Replication.Type replicationType() { return replicationType_; }
        public double gap() { return gap_; }
	}
}
