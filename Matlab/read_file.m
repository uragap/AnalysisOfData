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
    figure;
    plot(QTDate(:,2),QTDate(:,1),'*'); 
    title (strcat('Building #',x(11))) % Заголовок графика. 
    xlabel('Temperature') % Подпись по оси x. 
    ylabel('Energy') % Подпись по оси y.
    grid on;
    X = QTDate(:,2);
    Y = QTDate(:,1);

% datetick('x','dd-mm-yy')
