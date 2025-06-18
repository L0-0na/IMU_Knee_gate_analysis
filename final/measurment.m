function [degreesx,x] = measurment(t,K,a)
  
for k=1:K
    tic
    j=1;
    while toc<t
        v=readVoltage(a,'A0');
        degreesx(j,k)=v;
        x(j,k)=toc;
        j=j+1;
    end
    
end


    end

