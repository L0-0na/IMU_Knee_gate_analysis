%% Analyzing Gyroscopes data during statical position
% 10 different experiances was done each recorded data for about 14 second
T = 0:0.01:13.83;
N = 1:1:10;
[t,n] = meshgrid(T,N);

% Gyroscope on Xaxis
Gyro_X1 = xlsread('444.xlsx','AL2:AU1385');
mesh(t,n,Gyro_X1');
ylabel('Sample');
xlabel('time(sec)');
zlabel('Xaxis Gyroscope(dpm)');
figure;
% Gyroscope on Yaxis
Gyro_Y1 = xlsread('444.xlsx','AX2:BG1385');
mesh(t,n,Gyro_Y1');
ylabel('Sample');
xlabel('time(sec)');
zlabel('Yaxis Gyroscope(dpm)');
figure;
% Gyroscope on Zaxis
Gyro_Z1 = xlsread('444.xlsx','BJ2:BS1385');
mesh(t,n,Gyro_Z1');
ylabel('Sample');
xlabel('time(sec)');
zlabel('Zaxis Gyroscope(dpm)');

figure;
meanX_T = mean(Gyro_X1);
meanX_N = mean(Gyro_X1,2);

meanY_T = mean(Gyro_Y1);
meanY_N = mean(Gyro_Y1,2);

meanZ_T = mean(Gyro_Z1);
meanZ_N = mean(Gyro_Z1,2);

subplot(3,2,1)
plot(N,meanX_T);
title('time mean Xaxis');

subplot(3,2,2)
plot(T,meanX_N);
title('sample mean Xaxis');

subplot(3,2,3)
plot(N,meanY_T);
title('time mean Yaxis');

subplot(3,2,4)
plot(T,meanY_N);
title('sample mean Yaxis');

subplot(3,2,5)
plot(N,meanZ_T);
title('time mean Zaxis');

subplot(3,2,6)
plot(T,meanZ_N);
title('sample mean Zaxis');

%% studying the variance for Gyroscope
i = 1;
segX = zeros(1,139);
segY = zeros(1,139);
segZ = zeros(1,139);

for vec = 1:10:1384
    segX(i) = var(meanX_N(1:vec));
    segY(i) = var(meanY_N(1:vec));
    segZ(i) = var(meanZ_N(1:vec));
    i = i + 1;
end

subplot(3,1,1)
plot(segX(4:end));
title('variance estimation of the noise on Xaxis with changing N');

subplot(3,1,2)
plot(segY(4:end));
title('variance estimation of the noise on Yaxis with changing N');

subplot(3,1,3)
plot(segZ(4:end));
title('variance estimation of the noise on Zaxis with changing N');

%% Allan Deviation for Gyroscops
fs = 100;
pts = 110;
[taux,sigmax] = allan(meanX_N,fs,pts);
[tauy,sigmay] = allan(meanY_N,fs,pts);
[tauz,sigmaz] = allan(meanZ_N,fs,pts);
loglog(taux,sigmax,'b-s',tauy,sigmay,'r-o',tauz,sigmaz,'g-^');
title('Mean Allan deviation for Gyroscope');
xlabel('Time Cluster Size(sec)');
ylabel('Allan Deviation (dps)');
legend('Allan Dev - f_x','Allan Dev - f_y','Allan Dev - f_z');
grid on

% Allan deviation for Xaxis
N = 2.968*10^(-3);
B = 4.5*10^(-3);
K = 5.1*10^(-5);
slop_1 = N./sqrt(taux);
slop_0 = B*sqrt(2*log(2)/pi);
slop_2 = K*sqrt(taux/3);

subplot(3,1,1);
p1 = loglog(taux,sigmax,'b-s'); hold on;
p2 = loglog(taux,slop_1,'r-o');
p3 = loglog(taux,slop_0,'g-^');hold off;
%p4 = loglog(taux,slop_2,'g-x');hold off;
grid on;
xlabel('Time Cluster Size(sec)');
ylabel('Allan Deviation (dps)');
ylim([1.2*10^(-3),3.5*10^(-2)]);
text = {'Allan Dev - f_x','slop(-1/2): White noise/Angle(velosity)random walk','slop(0): Bias instability'};
P = [p1(1), p2(1), p3(1)];
legend(P,text);

% Allan deviation for Yaxis
N = 3.256*10^(-3);
B = 2.45*10^(-3);
slop_1Y = N./sqrt(tauy);
slop_0Y = B*sqrt(2*log(2)/pi);

subplot(3,1,2);
p4 = loglog(tauy,sigmay,'b-s'); hold on;
p5 = loglog(tauy,slop_1Y,'r-o');
p6 = loglog(tauy,slop_0Y,'g-^');hold off;
grid on;
xlabel('Time Cluster Size(sec)');
ylabel('Allan Deviation (dps)');
ylim([7*10^(-4),4*10^(-2)]);
text = {'Allan Dev - f_y','slop(-1/2): White noise/Angle(velosity)random walk','slop(0): Bias instability'};
P = [p4(1), p5(1), p6(1)];
legend(P,text);

% Allan deviation for Zaxis
N = 2.818*10^(-3);
B = 1.6*10^(-3);
slop_1Z = N./sqrt(tauz);
slop_0Z = B*sqrt(2*log(2)/pi);

subplot(3,1,3);
p7 = loglog(tauz,sigmaz,'b-s'); hold on;
p8 = loglog(tauz,slop_1Z,'r-o');
p9 = loglog(tauz,slop_0Z,'g-^');hold off;
grid on;
xlabel('Time Cluster Size(sec)');
ylabel('Allan Deviation (dps)');
ylim([9*10^(-4),3*10^(-2)]);
text = {'Allan Dev - f_z','slop(-1/2): White noise/Angle(velosity)random walk','slop(0): Bias instability'};
P = [p7(1), p8(1), p9(1)];
legend(P,text);