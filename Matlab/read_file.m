function QTDate = read_file(x)
    fid = fopen(x,'r'); % Открытие файла для чтения.
    QTDate = [];
    while true
        str = fgetl(fid); 
        if ~ischar(str)
            break
        else
            brk = find(str==9);% Массив позиций табуляций в строке.
            sqd = str(brk(2)+1:brk(3)-1);
            comma = (sqd ==',');
            sqd(comma) = '.';
            sqr = str(brk(3)+1:brk(4)-1);
            comma = (sqr ==',');
            sqr(comma) = '.';
            q = str2double(sqd)-str2double(sqr);
            if q < 0 || q > 1.5e+9   
                continue;
            end
            st = str(brk(6)+1:end);
            comma = (st ==',');
            st(comma) = '.';
            t = str2double(st);
            date = datenum(str(1:brk(1)-1), 'dd.mm.yyyy HH:MM:ss');
            Temp = [q,t,date];
            QTDate = [QTDate; Temp]; %доб строки           
        end
    end     
    fclose(fid); % Закрытие файла.
    % Графическое отображение данных
    X = QTDate(:,2);
    Y = QTDate(:,1);
    figure;
    plot(X,Y,'*'); 
    title (strcat('Здание #',x(40))) % Заголовок графика. 
    xlabel('Наружная температура, °C') % Подпись по оси x. 
    ylabel('Энергия, Кал') % Подпись по оси y.
    grid on;
    hold on;
    S = mean(X.^2)-mean(X)^2;
    a = (mean(Y.*X) - mean(X)*mean(Y))/S;
    b = mean(Y)-a*mean(X);
    x = -20:0.1:20;
    F = MyLinear(a,b,x);
    plot(x,F);
    

% datetick('x','dd-mm-yy')
