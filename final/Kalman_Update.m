function ret = Kalman_Update(newValue, newRate)
dt = 0.03;
global Q_angle;
global Q_bias;
global R_measure;
global K_angle;
global K_bias;
global P;

K_rate = newRate - K_bias;
K_angle = K_angle + dt * K_rate;
P(1,1) = P(1,1) + dt * (P(2,2) + P(1,2)) + Q_angle * dt;
P(1,2) = P(1,2) - dt * P(2,2);
P(2,1) = P(2,1) - dt * P(2,2);
P(2,2) = P(2,2) + Q_bias * dt;

S = P(1,1) + R_measure;
K(1) = P(1,1) / S;
K(2) = P(2,1) / S;

y = newValue - K_angle;

K_angle = K_angle + K(1) * y;
K_bias = K_bias + K(2) * y;

P(1,1) = P(1,1) - K(1)*P(1,1);
P(1,2) = P(1,2) - K(1)*P(1,2);
P(2,1) = P(2,1) - K(2)*P(1,1);
P(2,2) = P(2,2) - K(2)*P(1,2);

ret = K_angle;
end

