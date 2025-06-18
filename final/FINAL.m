%% Offset
AccX1_offset = 0.0204;
AccY1_offset = -0.057;
AccZ1_offset = -0.0395;
GyrX1_offset = -5.1771;
GyrY1_offset = -0.5736;
GyrZ1_offset = 0.0362;

AccX2_offset = 0.0195;
AccY2_offset = -0.0742;
AccZ2_offset = 0.0233;
GyrX2_offset = -1.5651;
GyrY2_offset = -0.3998;
GyrZ2_offset = -0.6439;
%% Luna Walking Experience
% acceleration and gyro recorded in different experement
%Acceleration
t = 0:0.03:56.96;
Acc_X1 = xlsread('luna.xlsx','A1:A1899')-AccX1_offset;
Acc_Y1 = xlsread('luna.xlsx','B1:B1899')-AccY1_offset;
Acc_Z1 = xlsread('luna.xlsx','C1:C1899')-AccZ1_offset;
Acc_X2 = xlsread('luna.xlsx','D1:D1899')-AccX2_offset;
Acc_Y2 = xlsread('luna.xlsx','E1:E1899')-AccY2_offset;
Acc_Z2 = xlsread('luna.xlsx','F1:F1899')-AccZ2_offset;

% Gyroscopes
t1 = 0:0.03:56.53;
Gyro_X1 = xlsread('luna.xlsx','G1:G1885')-GyrX1_offset;
Gyro_Y1 = xlsread('luna.xlsx','H1:H1885')-GyrY1_offset;
Gyro_Z1 = xlsread('luna.xlsx','I1:I1885')-GyrZ1_offset;
Gyro_X2 = xlsread('luna.xlsx','J1:J1885')-GyrX2_offset;
Gyro_Y2 = xlsread('luna.xlsx','K1:K1885')-GyrY2_offset;
Gyro_Z2 = xlsread('luna.xlsx','L1:L1885')-GyrZ2_offset;
%% 
subplot(2,3,1);
plot(t1,Gyro_X2);
title("Gyroscope X axis");

subplot(2,3,2);
plot(t1,Gyro_Y2);
title("Gyroscope Y axis");

subplot(2,3,3);
plot(t1,Gyro_Z2);
title("Gyroscope Z axis");

subplot(2,3,4);
plot(t,Acc_X2);
title("Accelerometer X axis");

subplot(2,3,5);
plot(t,Acc_Y2);
title("Accelerometer Y axis");

subplot(2,3,6);
plot(t,Acc_Z2);
title("Accelerometer Z axis");

figure;

subplot(2,3,1);
plot(t1,Gyro_X1);
title("Gyroscope X axis");

subplot(2,3,2);
plot(t1,Gyro_Y1);
title("Gyroscope Y axis");

subplot(2,3,3);
plot(t1,Gyro_Z1);
title("Gyroscope Z axis");

subplot(2,3,4);
plot(t,Acc_X1);
title("Accelerometer X axis");

subplot(2,3,5);
plot(t,Acc_Y1);
title("Accelerometer Y axis");

subplot(2,3,6);
plot(t,Acc_Z1);
title("Accelerometer Z axis");
%% Angle measur
% Acceleration
roll_1= atan2(Acc_Y1,Acc_Z1)*180/pi;
W = sqrt(Acc_Y1.*Acc_Y1 + Acc_Z1.*Acc_Z1);
heading_1 = atan2(-1*Acc_X1,W)*180/pi;

roll_2 = atan2(Acc_Y2,Acc_Z2)*180/pi;
W2 = sqrt(Acc_Y2.*Acc_Y2 + Acc_Z2.*Acc_Z2);
heading_2 = atan2(-1*Acc_X2,W2)*180/pi;

N = 1885;
% Gyroscope
roll_1_gyr = zeros(1,N);
roll_2_gyr = zeros(1,N);
heading_1_gyr = zeros(1,N);
heading_2_gyr = zeros(1,N);
dt = 0.03;
for j=2:N
    roll_1_gyr(j) = roll_1_gyr(j-1) + Gyro_X1(j-1)*dt;
    roll_2_gyr(j) = roll_2_gyr(j-1) + Gyro_X2(j-1)*dt;
    heading_1_gyr(j) = heading_1_gyr(j-1) + Gyro_Z1(j-1)*dt;
    heading_2_gyr(j) = heading_2_gyr(j-1) + Gyro_Z2(j-1)*dt;
end

subplot(2,2,1);
plot(t,roll_1,t1,roll_1_gyr);
title("roll_1");
ylabel('degree');
xlabel('time(sec)');

subplot(2,2,2);
plot(t,heading_1,t1,heading_1_gyr);
title("heading_1");
ylabel('degree');
xlabel('time(sec)');

subplot(2,2,3);
plot(t,roll_2,t1,roll_2_gyr);
title("roll_2");
ylabel('degree');
xlabel('time(sec)');
legend('Acceleration measurement','Gyroscope measurement');

subplot(2,2,4);
plot(t,heading_2,t1,heading_2_gyr);
title("heading_2");
ylabel('degree');
xlabel('time(sec)');
legend('Acceleration measurement','Gyroscope measurement');
%% 
Hip1 = roll_1;
Knee1 = roll_2 - roll_1;

Hip2 = roll_1_gyr;
Knee2 = roll_2_gyr - roll_1_gyr;
subplot(2,1,1);
plot(t,Hip1,t1,Hip2);
title("Hip");
ylabel('degree');
xlabel('time(sec)');
legend('Acceleration measurement','Gyroscope measurement');

subplot(2,1,2);
plot(t,Knee1,t1,Knee2);
title("Knee");
ylabel('degree');
xlabel('time(sec)');
legend('Acceleration measurement','Gyroscope measurement');
%% LSE
N1 = 1899;
N2 = 1885;

A_hip = zeros(1,N1);
Jmin_hip = zeros(1,N1);
A_hip(1) = Hip1(1);
Jmin_hip(1) = 0;

A_knee = zeros(1,N1);
Jmin_knee = zeros(1,N1);
A_knee(1) = Knee1(1);
Jmin_knee(1) = 0;

for h=2:(N1)
    s = Hip(h);
    A_hip(h) = A_hip(h-1)+(1/h)*(s-A_hip(h-1));
    Jmin_hip(h) = Jmin_hip(h-1)+((h-1)/(h))*(s-A_hip(h-1))^2;
     s1 = Knee1(h);
    A_knee(h) = A_knee(h-1)+(1/h)*(s1-A_knee(h-1));
    Jmin_knee(h) = Jmin_knee(h-1)+((h-1)/(h))*(s1-A_knee(h-1))^2;
end
subplot(2,1,1);
plot(t,Hip1,t,A_hip);
title("Hip");
ylabel('degree');
xlabel('time(sec)');
legend('Acceleration measurement','LSE estimation');

subplot(2,1,2);
plot(t,Knee1,t,A_knee);
title("Knee");
ylabel('degree');
xlabel('time(sec)');
legend('Acceleration measurement','LSE estimation');

% jmin
subplot(2,1,1);
plot(t,Jmin_hip);
title("J_{min} for hip");

subplot(2,1,2);
plot(t,Jmin_knee);
title("J_{min} for knee");

%% Kalman
% kalman for Xaxis
Kalman_filter(0.001,0.003,0.03);
kalRoll_1 = zeros(1,N1);

for i=2:N2
    kalRoll_1(i) = Kalman_Update(roll_1(i),Gyro_X1(i));
end

subplot(3,1,1);
plot(t,roll_1,t1,roll_1_gyr,t,kalRoll_1);
title("filtering roll");
ylabel('degree');
xlabel('time(sec)');
legend('Roll values from Accelerometer','Roll values for Gyroscope','Kalman filtered Roll');


Kalman_filter(0.001,0.003,0.03);
kalRoll_2 = zeros(1,N1);
for i=2:N2
    kalRoll_2(i) = Kalman_Update(roll_2(i),Gyro_X2(i));
end
subplot(3,1,2);
plot(t,roll_2,t1,roll_2_gyr,t,kalRoll_2);
title("filtering roll");
ylabel('degree');
xlabel('time(sec)');
legend('Roll values from Accelerometer','Roll values for Gyroscope','Kalman filtered Roll');

subplot(3,1,3);
KNEE_acc = 180 - roll_1 + roll_2;
KNEE_gyr = 180 - roll_1_gyr + roll_2_gyr;
KNEE_kal = 180 - kalRoll_1 + kalRoll_2;
plot(t,KNEE_acc,t1,KNEE_gyr,t,KNEE_kal);
title("filtering roll");
ylabel('degree');
xlabel('time(sec)');
legend('knee values from Accelerometer','knee values for Gyroscope','Kalman filtered knee');

%%
for i = 1:749
    
R_roll_1 = [1 0 0; 0 cos(roll_1(i)) -sin(roll_1(i)); 0 sin(roll_1(i)) cos(roll_1(i))];
R_heading_1 = [cos(heading_1(i)) -sin(heading_1(i)) 0;sin(heading_1(i)) cos(heading_1(i)) 0; 0 0 1];
R_a = R_heading_1*R_roll_1;

R_roll_2 = [1 0 0; 0 cos(roll_2(i)) -sin(roll_2(i)); 0 sin(roll_2(i)) cos(roll_2(i))];
R_heading_2 = [cos(heading_2(i)) -sin(heading_2(i)) 0;sin(heading_2(i)) cos(heading_2(i)) 0; 0 0 1];
R_b = R_heading_2*R_roll_2;

end