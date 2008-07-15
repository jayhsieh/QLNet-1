﻿/*
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
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QLNet;

namespace TestSuite {
    [TestClass()]
    public class T_Matrices {

        int N;
        Matrix M1, M2, M3, M4, M5, M6, M7, I;

        double norm(Vector v) {
            return Math.Sqrt(Vector.DotProduct(v,v));
        }

        double norm(Matrix m) {
            double sum = 0.0;
            for (int i=0; i<m.rows(); i++)
                for (int j=0; j<m.columns(); j++)
                    sum += m[i,j]*m[i,j];
            return Math.Sqrt(sum);
        }

        void setup() {

            N = 3;
            M1 = new Matrix(N,N); M2 = new Matrix(N,N); I = new Matrix(N,N);
            M3 = new Matrix(3, 4);
            M4 = new Matrix(4, 3);
            M5 = new Matrix(4, 4);
            M6 = new Matrix(4, 4);

            M1[0,0] = 1.0;  M1[0,1] = 0.9;  M1[0,2] = 0.7;
            M1[1,0] = 0.9;  M1[1,1] = 1.0;  M1[1,2] = 0.4;
            M1[2,0] = 0.7;  M1[2,1] = 0.4;  M1[2,2] = 1.0;

            M2[0,0] = 1.0;  M2[0,1] = 0.9;  M2[0,2] = 0.7;
            M2[1,0] = 0.9;  M2[1,1] = 1.0;  M2[1,2] = 0.3;
            M2[2,0] = 0.7;  M2[2,1] = 0.3;  M2[2,2] = 1.0;

            I[0,0] = 1.0;  I[0,1] = 0.0;  I[0,2] = 0.0;
            I[1,0] = 0.0;  I[1,1] = 1.0;  I[1,2] = 0.0;
            I[2,0] = 0.0;  I[2,1] = 0.0;  I[2,2] = 1.0;

            M3[0,0] = 1; M3[0,1] = 2; M3[0,2] = 3; M3[0,3] = 4;
            M3[1,0] = 2; M3[1,1] = 0; M3[1,2] = 2; M3[1,3] = 1;
            M3[2,0] = 0; M3[2,1] = 1; M3[2,2] = 0; M3[2,3] = 0;

            M4[0,0] = 1;  M4[0,1] = 2;  M4[0,2] = 400;
            M4[1,0] = 2;  M4[1,1] = 0;  M4[1,2] = 1;
            M4[2,0] = 30; M4[2,1] = 2;  M4[2,2] = 0;
            M4[3,0] = 2;  M4[3,1] = 0;  M4[3,2] = 1.05;

            // from Higham - nearest correlation matrix
            M5[0,0] = 2;   M5[0,1] = -1;  M5[0,2] = 0.0; M5[0,3] = 0.0;
            M5[1,0] = M5[0,1];  M5[1,1] = 2;   M5[1,2] = -1;  M5[1,3] = 0.0;
            M5[2,0] = M5[0,2]; M5[2,1] = M5[1,2];  M5[2,2] = 2;   M5[2,3] = -1;
            M5[3,0] = M5[0,3]; M5[3,1] = M5[1,3]; M5[3,2] = M5[2,3];  M5[3,3] = 2;

            // from Higham - nearest correlation matrix to M5
            M6[0,0] = 1;        M6[0,1] = -0.8084124981;  M6[0,2] = 0.1915875019;   M6[0,3] = 0.106775049;
            M6[1,0] = M6[0,1]; M6[1,1] = 1;        M6[1,2] = -0.6562326948;  M6[1,3] = M6[0,2];
            M6[2,0] = M6[0,2]; M6[2,1] = M6[1,2]; M6[2,2] = 1;        M6[2,3] = M6[0,1];
            M6[3,0] = M6[0,3]; M6[3,1] = M6[1,3]; M6[3,2] = M6[2,3]; M6[3,3] = 1;

            M7 = M1;
            M7[0,1] = 0.3; M7[0,2] = 0.2; M7[2,1] = 1.2;
        }

        [TestMethod()]
        public void testEigenvectors() {
            //("Testing eigenvalues and eigenvectors calculation...");

            setup();

            Matrix[] testMatrices = { M1, M2 };

            for (int k=0; k<testMatrices.Length; k++) {

                Matrix M = testMatrices[k];
                SymmetricSchurDecomposition dec = new SymmetricSchurDecomposition(M);
                Vector eigenValues = dec.eigenvalues();
                Matrix eigenVectors = dec.eigenvectors();
                double minHolder = double.MaxValue;

                for (int i=0; i<N; i++) {
                    Vector v = new Vector(N);
                    for (int j = 0; j < N; j++)
                        v[j] = eigenVectors[j,i];
                    // check definition
                    Vector a = M * v;
                    Vector b = eigenValues[i] * v;
                    if (norm(a-b) > 1.0e-15)
                        Assert.Fail("Eigenvector definition not satisfied");
                    // check decreasing ordering
                    if (eigenValues[i] >= minHolder) {
                        Assert.Fail("Eigenvalues not ordered: " + eigenValues);
                    } else
                        minHolder = eigenValues[i];
                }

                // check normalization
                Matrix m = eigenVectors * Matrix.transpose(eigenVectors);
                if (norm(m-I) > 1.0e-15)
                    Assert.Fail("Eigenvector not normalized");
            }
        }

    }
}
