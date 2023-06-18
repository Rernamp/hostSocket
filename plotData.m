clear;
close all;


data = csvread("F:\MyFolder\GitProject\hostSocket\bin\Debug\net6.0\damp.txt");

temp = [];

plot(data)

%%

temp1 = data - mean(data) .* ones(length(data), 1);

temp1 = temp1 ./ max(abs(temp1));

plot(temp1)

