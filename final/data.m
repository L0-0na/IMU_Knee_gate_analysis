%% Analyzing Accelerometers data during statical position
% 10 different experiances was done each recorded data for about 12 second
T = 0:0.01:11.13;
N = 1:1:10;
[t,n] = meshgrid(T,N);

% Acceleration on Xaxis
Acc_X1 = xlsread('nn...xlsx','AK2:AT1115');
mesh(t,n,Acc_X1');
ylabel('Sample');
xlabel('time(sec)');
zlabel('Acceleration(m/s^2)');

% Acceleration on Yaxis
Acc_Y1 = xlsread('nn...xlsx','AW2:BF1115');
mesh(t,n,Acc_Y1');
ylabel('Sample');
xlabel('time(sec)');
zlabel('Acceleration(m/s^2)');

% Acceleration on Zaxis
Acc_Z1 = xlsread('nn...xlsx','BI2:BR1115');
mesh(t,n,Acc_Z1');
ylabel('Sample');
xlabel('time(sec)');
zlabel('Acceleration(m/s^2)');

meanX_T = mean(Acc_X1);
meanX_N = mean(Acc_X1,2);

meanY_T = mean(Acc_Y1);
meanY_N = mean(Acc_Y1,2);

meanZ_T = mean(Acc_Z1);
meanZ_N = mean(Acc_Z1,2);

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

%% studying the variance for Accelerometer
i = 1;
segX = zeros(1,112);
segY = zeros(1,112);
segZ = zeros(1,112);
for vec = 1:10:1114
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

%% Allan Deviation for Accelerometer
fs = 100;
pts = 100;
[taux,sigmax] = allan(meanX_N,fs,pts);
[tauy,sigmay] = allan(meanY_N,fs,pts);
[tauz,sigmaz] = allan(meanZ_N,fs,pts);
loglog(taux,sigmax,'b-s',tauy,sigmay,'r-o',tauz,sigmaz,'y-d');
title('Mean Allan deviation for Accelerometer');
xlabel('Time Cluster Size(sec)');
ylabel('Allan Deviation (m\s^2)');
legend('Allan Dev - f_x','Allan Dev - f_y','Allan Dev - f_z');
grid on

% Allan deviation for Xaxis
N = 1.1*10^(-4);
B = 0.8*10^(-4);
K = 5.1*10^(-5);
slop_1 = N./sqrt(taux);
slop_0 = B*sqrt(2*log(2)/pi);
slop_2 = K*sqrt(taux/3);

subplot(3,1,1);
p1 = loglog(taux,sigmax,'b-s'); hold on;
p2 = loglog(taux,slop_1,'r-o');
p3 = loglog(taux,slop_0,'y-d');
p4 = loglog(taux,slop_2,'g-x');hold off;
grid on;
xlabel('Time Cluster Size(sec)');
ylabel('Allan Deviation (m\s^2)');
ylim([3.5*10^(-5),1.2*10^(-3)]);
text = {'Allan Dev - f_x','slop(-1/2): White noise/Angle(velosity)random walk','slop(0): Bias instability','slop(+1/2): Random walk'};
P = [p1(1), p2(1), p3(1), p4(1)];
legend(P,text);

% Allan deviation for Yaxis
N = 1.1*10^(-4);
B = 0.35*10^(-4);
slop_1Y = N./sqrt(tauy);
slop_0Y = B*sqrt(2*log(2)/pi);

subplot(3,1,2);
p4 = loglog(tauy,sigmay,'b-s'); hold on;
p5 = loglog(tauy,slop_1Y,'r-o');
p6 = loglog(tauy,slop_0Y,'y-d');hold off;
grid on;
xlabel('Time Cluster Size(sec)');
ylabel('Allan Deviation (m\s^2)');
ylim([10^(-5),1.2*10^(-3)]);
text = {'Allan Dev - f_y','slop(-1/2): White noise/Angle(velosity)random walk','slop(0): Bias instability'};
P = [p4(1), p5(1), p6(1)];
legend(P,text);

% Allan deviation for Zaxis
N = 1.5*10^(-4);
B = 9*10^(-5);
slop_1Z = N./sqrt(tauz);
slop_0Z = B*sqrt(2*log(2)/pi);

subplot(3,1,3);
p7 = loglog(tauz,sigmaz,'b-s'); hold on;
p8 = loglog(tauz,slop_1Z,'r-o');
p9 = loglog(tauz,slop_0Z,'y-d');hold off;
grid on;
xlabel('Time Cluster Size(sec)');
ylabel('Allan Deviation (m\s^2)');
ylim([3*10^(-5),2*10^(-3)]);
text = {'Allan Dev - f_z','slop(-1/2): White noise/Angle(velosity)random walk','slop(0): Bias instability'};
P = [p7(1), p8(1), p9(1)];
legend(P,text);