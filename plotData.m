clear;
close all;


data = csvread("F:\MyFolder\GitProject\hostSocket\bin\Debug\net6.0\damp.txt");

temp = [];
%%
numberElements = data(1);

numberElements = sum(bitget(numberElements, 1:4));

data = data(2:end);

data = data(1:fix(length(data) / numberElements) * numberElements);

dataOfArray = reshape(data, numberElements, length(data) / numberElements);

dataOfArray = dataOfArray(:, 600:end);

temp1 = dataOfArray - mean(dataOfArray, 2) .* ones(length(dataOfArray(:,1)), length(dataOfArray(1,:)));

temp1 = temp1 ./ max(abs(temp1'))';


figure()
plot(dataOfArray')

figure()
plot(temp1')

%%

% [y, W ] = func_LC_NLMS(temp1, 32, numberElements, 0.1);
[y, W ] = func_LC_RLS(temp1, 32, numberElements);


figure()
plot(y)

y = y ./ max(y);

figure()
plot(y)

%%

figure()
hold on
plot(temp1(2,:))
plot(y)

legend("Input channel", "Output")


