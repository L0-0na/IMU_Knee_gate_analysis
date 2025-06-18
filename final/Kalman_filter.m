function Kalman_filter(angle,bias,measure)
    global Q_angle;
    global Q_bias;
    global R_measure;
    global K_angle;
    global K_bias;
    global P;
    
    Q_angle = angle;
    Q_bias = bias;
    R_measure = measure;

    K_angle = 0;
    K_bias = 0;
    P = zeros(2,2);
end

