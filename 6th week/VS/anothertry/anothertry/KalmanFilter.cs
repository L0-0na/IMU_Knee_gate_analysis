using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anothertry
{
    public class KalmanFilter
    {
        public double Q_angle, Q_bias, R_measure;
        private double K_angle = 0, K_bias = 0, K_rate,S,y, dt = 0.01;
        private double[,] P = new double[,] { { 0, 0 }, { 0, 0 } };
        private double[] K = new double[2];
        public KalmanFilter(double angle, double bias, double measure)
        {
            Q_angle = angle;
            Q_bias = bias;
            R_measure = measure;
        }
        
        public double Update(double newValue, double newRate)
        {
            K_rate = newRate - K_bias;
            K_angle += dt * K_rate;
            P[0,0] += dt * (P[1,1] + P[0,1]) + Q_angle * dt;
            P[0,1] -= dt * P[1,1];
            P[1,0] -= dt * P[1,1];
            P[1,1] += Q_bias * dt;

            S = P[0,0] + R_measure;

            K[0] = P[0,0] / S;
            K[1] = P[1,0] / S;

            y = newValue - K_angle;

            K_angle += K[0] * y;
            K_bias += K[1] * y;

            P[0,0] -= K[0] * P[0,0];
            P[0,1] -= K[0] * P[0,1];
            P[1,0] -= K[1] * P[0,0];
            P[1,1] -= K[1] * P[0,1];
            return K_angle;
        }
    }
}
