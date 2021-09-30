--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--Этап 0: Инициализация.
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--Переменные.
DECLARE @identifiedPerson INT --Идентифицированный человек.
DECLARE @identifiedSocServ INT --Идентифицированное социальное обслуживание.
DECLARE @yearReport_INT INT --Год, конвертированный в число.
DECLARE @monthReport_INT INT --Месяц, конвертированный в число.
DECLARE @startDate DATE --Дата -начала периода.
DECLARE @endDate DATE --Дата конца периода.
DECLARE @thereIsError INT = 0 --Флаг наличия ошибки.
DECLARE @message VARCHAR(256)= 'Успешно' --Сообщение.
--Удаление временных таблиц.
IF OBJECT_ID('tempdb..#FOUND_PEOPLE') IS NOT NULL BEGIN DROP TABLE #FOUND_PEOPLE END --Найденные люди по входным данным.
IF OBJECT_ID('tempdb..#FOUND_SOC_SERV') IS NOT NULL BEGIN DROP TABLE #FOUND_SOC_SERV END --Найденные социальные обслуживания по входным данным.
IF OBJECT_ID('tempdb..#DATA_FOR_INSERV') IS NOT NULL BEGIN DROP TABLE #DATA_FOR_INSERV END --Данные для вставки.
IF OBJECT_ID('tempdb..#FOUND_AGREGATION') IS NOT NULL BEGIN DROP TABLE #FOUND_AGREGATION END --Найденные агрегации услуг.
--Создание временных таблиц.
CREATE TABLE #FOUND_PEOPLE (
    PERSONOUID INT, --Идентификатор личного дела.
)
CREATE TABLE #FOUND_SOC_SERV (
    SOC_SERV_OUID INT, --Идентификатор социального обслуживания.
)
CREATE TABLE #DATA_FOR_INSERV (
    TYPE_SERV_CODE VARCHAR(256), --Код услуги
    TYPE_SERV_NAME VARCHAR(256), --Наименование услуги
    COUNT_SERV_NORMAL INT, --Количество оказанных услуг в месяц в рамках норматива
    COUNT_SERV_OVER INT --Количество оказанных услуг в месяц сверх норматива
)
CREATE TABLE #FOUND_AGREGATION (
    TYPE_SERV_CODE VARCHAR(256), --Код услуги
    TYPE_SERV_NAME VARCHAR(256), --Наименование услуги
    COUNT_SERV_NORMAL INT, --Количество оказанных услуг в месяц в рамках норматива
    COUNT_SERV_OVER INT, --Количество оказанных услуг в месяц сверх норматива
    SOC_SERV_AGR_OUID INT, --Идентификатор агрегации услуги.
    CONDITION VARCHAR(256)--Условия оказания социальных услуг.
)
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--Этап 1: Установка входных параметров.
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--Информация о получателе.
DECLARE @SNILS VARCHAR(256) SET @SNILS = '#SNILS#' --СНИЛС.
DECLARE @name VARCHAR(256) SET @name = '#name#' --Имя.
DECLARE @surname VARCHAR(256) SET @surname = '#surname#' --Фамилия.
DECLARE @secondname VARCHAR(256) SET @secondname = '#secondname#' --Отчество.
DECLARE @birthdate VARCHAR(256) SET @birthdate = '#birthdate#' --Дата рождения.
--Данные об индивидуальной программе получателя социальных услуг (ИППСУ).
DECLARE @formSocServ VARCHAR(256) SET @formSocServ = '#formSocServ#' --Форма социального обслуживания.
DECLARE @dateRegistration VARCHAR(256) SET @dateRegistration = '#dateRegistration#' --Дата оформления.
DECLARE @numberDocumentIPRA VARCHAR(256) SET @numberDocumentIPRA = '#numberDocumentIPRA#' --Номер документа
--Период, за который предоставляются сведения.
DECLARE @yearReport VARCHAR(256) SET @yearReport = '#yearReport#' --Год.
DECLARE @monthReport VARCHAR(256) SET @monthReport = (SELECT A_CODE FROM SPR_MONTH WHERE A_NAME = '#monthReport#' OR CONVERT(VARCHAR, A_CODE) = '#monthReport#') --Месяц.
--Данные об услугах.
INSERT INTO #DATA_FOR_INSERV (TYPE_SERV_CODE, TYPE_SERV_NAME, COUNT_SERV_NORMAL, COUNT_SERV_OVER)
VALUES
    <DATA_FOR_INSERV ('#TYPE_SERV_CODE#', '#TYPE_SERV_NAME#', '#COUNT_SERV_NORMAL#', '#COUNT_SERV_OVER#')>
DELETE FROM #DATA_FOR_INSERV
WHERE TYPE_SERV_CODE = ''
--Проверка исходных данных.
IF ((SELECT LEN(@SNILS)) = 0 AND @thereIsError = 0) BEGIN SET @thereIsError = 1 SET @message = 'Не указан СНИЛС' END
IF ((SELECT LEN(@name)) = 0 AND @thereIsError = 0) BEGIN SET @thereIsError = 1 SET @message = 'Не указано имя' END
IF ((SELECT LEN(@surname)) = 0 AND @thereIsError = 0) BEGIN SET @thereIsError = 1 SET @message = 'Не указана фамилия' END
IF ((SELECT LEN(@secondname)) = 0 AND @thereIsError = 0) BEGIN SET @thereIsError = 1 SET @message = 'Не указано отчество' END
IF ((SELECT LEN(@birthdate)) = 0 AND @thereIsError = 0) BEGIN SET @thereIsError = 1 SET @message = 'Не указана дата рождения' END
IF ((SELECT LEN(@formSocServ)) = 0 AND @thereIsError = 0) BEGIN SET @thereIsError = 1 SET @message = 'Не указана форма социального обслуживания' END
IF ((SELECT LEN(@dateRegistration)) = 0 AND @thereIsError = 0) BEGIN SET @thereIsError = 1 SET @message = 'Не указана дата оформления документа ИППСУ' END
IF ((SELECT LEN(@numberDocumentIPRA)) = 0 AND @thereIsError = 0) BEGIN SET @thereIsError = 1 SET @message = 'Не указан номер документа ИППСУ' END
IF ((SELECT ISDATE(@birthdate)) = 0 AND @thereIsError = 0) BEGIN SET @thereIsError = 1 SET @message = 'Не верный формат даты дня рождения' END
IF ((SELECT ISDATE(@dateRegistration)) = 0 AND @thereIsError = 0) BEGIN SET @thereIsError = 1 SET @message = 'Не верный формат даты оформления ИППСУ' END
IF ((SELECT ISNUMERIC(@yearReport)) = 0 AND @thereIsError = 0) BEGIN SET @thereIsError = 1 SET @message = 'Не верный формат года' END
IF ((SELECT ISNUMERIC(@monthReport)) = 0 AND @thereIsError = 0) BEGIN SET @thereIsError = 1 SET @message = 'Не верный формат месяца' END
--Конвертация исходных данных.
IF (@thereIsError = 0) BEGIN
    SET @yearReport_INT = CONVERT(INT, @yearReport)
    SET @monthReport_INT = CONVERT(INT, @monthReport)
    SET @startDate = CAST(@yearReport + '-' + @monthReport + '-01' AS DATE)
    SET @endDate = DATEADD(MONTH, ((YEAR(@startDate) - 1900) * 12) + MONTH(@startDate), -1)
END
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--Этап 2: Идентификация человека.
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
IF (@thereIsError = 0) BEGIN
    --Выбор людей, удовлетворяющих условиям.
    INSERT #FOUND_PEOPLE (PERSONOUID)
    SELECT DISTINCT
        personalCard.OUID AS PERSONOUID
    FROM WM_PERSONAL_CARD personalCard --Личное дело гражданина.
    ----Фамилия.
        LEFT JOIN SPR_FIO_SURNAME fioSurname
            ON fioSurname.OUID = personalCard.SURNAME
    ----Имя.     
        LEFT JOIN SPR_FIO_NAME fioName
            ON fioName.OUID = personalCard.A_NAME 
    ----Отчество.   
        LEFT JOIN SPR_FIO_SECONDNAME fioSecondname
            ON fioSecondname.OUID = personalCard.A_SECONDNAME
    WHERE personalCard.A_STATUS = 10 --Статус личного дела в БД "Действует".
        AND personalCard.A_PCSTATUS = 1 --Статус личного дела "Действует".
        AND personalCard.A_SNILS = @SNILS --СНИЛС совпадает.
        AND ISNULL(personalCard.A_NAME_STR, fioName.A_NAME) = @name --Имя совпадает.
        AND ISNULL(personalCard.A_SURNAME_STR, fioSurname.A_NAME) = @surname --Фамилия совпадает.
        AND CONVERT(DATE, personalCard.BIRTHDATE) = CONVERT(DATE, @birthdate) --Дата рожденяи совпадает.
        AND ISNULL(personalCard.A_SECONDNAME_STR, fioSecondname.A_NAME) = @secondname --Отчество совпадает.
    --Подсчет людей.
    DECLARE @countFoundPeople INT
    SET @countFoundPeople = (SELECT COUNT(*) FROM #FOUND_PEOPLE)
    --Результат 2 этапа.
    IF (@countFoundPeople > 1) BEGIN
        SET @thereIsError = 1
        SET @message = 'Найдено более одного человека, удовлетворяющего условиям'
    END
    ELSE IF (@countFoundPeople = 0) BEGIN
        SET @thereIsError = 1
        SET @message = 'Не найден человек, удовлетворяющий условиям'
    END
    ELSE BEGIN
        SET @identifiedPerson = (SELECT TOP 1 PERSONOUID FROM #FOUND_PEOPLE)
    END 
END 
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--Этап 3: Идентификация социального обслуживания.
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
IF (@thereIsError = 0) BEGIN
    --Выбор назначений на социальной обслуживание.
    INSERT #FOUND_SOC_SERV(SOC_SERV_OUID)
    SELECT DISTINCT
        socServ.OUID AS SOC_SERV_OUID
    FROM ESRN_SOC_SERV socServ --Назначение социального обслуживания.
    ----Период предоставления МСП.        
        INNER JOIN SPR_SOCSERV_PERIOD period
            ON period.A_STATUS = 10 --Статус в БД "Действует".
                AND period.A_SERV = socServ.OUID --Связка с назначением.   
                AND @yearReport_INT BETWEEN YEAR(period.STARTDATE) AND ISNULL(YEAR(period.A_LASTDATE), 3000) --Год отчета входит в период действия назначения.
                AND (@yearReport_INT <> YEAR(period.STARTDATE) AND @yearReport_INT <> ISNULL(YEAR(period.A_LASTDATE), 3000) --Год отчета не равен крайнему.
                    OR @yearReport_INT = YEAR(period.STARTDATE) AND @monthReport_INT >= MONTH(period.STARTDATE) --Или равен начальному, но месяц позже начала.
                    OR @yearReport_INT = YEAR(period.A_LASTDATE) AND @monthReport_INT <= ISNULL(MONTH(period.A_LASTDATE), 12) --Или равен конечному, но месяц раньше конца.
                )
    ----Индивидуальная программа.
        INNER JOIN INDIVID_PROGRAM individProgram
            ON individProgram.A_OUID = socServ.A_IPPSU
                AND individProgram.A_STATUS = 10 --Статус индивидуальной программы в БД "Действует".
    ----Действующие документы.
        INNER JOIN WM_ACTDOCUMENTS actDocuments
            ON actDocuments.OUID = individProgram.A_DOC
                AND actDocuments.A_STATUS = 10 --Статус документа в БД "Действует".
                AND actDocuments.DOCUMENTSNUMBER = @numberDocumentIPRA --Номер документа совпадает с требуемым.
    ----Форма социального обслуживания.
        INNER JOIN SPR_FORM_SOCSERV formSocServ
            ON formSocServ.A_OUID = individProgram.A_FORM_SOCSERV
                AND formSocServ.A_NAME = @formSocServ --Совпадает с формой отчета.
    ----Органы социальной защиты.
        INNER JOIN SPR_ORG_BASE organization
            ON organization.OUID = socServ.A_ORGNAME
    WHERE socServ.A_STATUS = 10 --Статус назначения в БД "Действует".
        AND socServ.A_PERSONOUID = @identifiedPerson --Льготодержатель - идентифицированный человек.
    --Подсчет назначений.
    DECLARE @countFoundSocServ INT
    SET @countFoundSocServ = (SELECT COUNT(*) FROM #FOUND_SOC_SERV)
    --Результат 3 этапа.
    IF (@countFoundSocServ > 1) BEGIN
        SET @thereIsError = 1
        SET @message = 'Найдено более одного социального обслуживания, удовлетворяющего условиям'
    END
    ELSE IF (@countFoundSocServ = 0) BEGIN
        SET @thereIsError = 1
        SET @message = 'Не найдено социальное обслуживание, удовлетворяющего условиям'
    END
    ELSE BEGIN
        SET @identifiedSocServ = (SELECT TOP 1 SOC_SERV_OUID FROM #FOUND_SOC_SERV)
    END 
END
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--Этап 4: Идентификация агрегаций по услугам.
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
IF (@thereIsError = 0) BEGIN
    --Выбор агрегации по услуге.
    INSERT INTO #FOUND_AGREGATION(TYPE_SERV_CODE, TYPE_SERV_NAME, COUNT_SERV_NORMAL, COUNT_SERV_OVER, SOC_SERV_AGR_OUID, CONDITION)
    SELECT DISTINCT
        forInsert.TYPE_SERV_CODE    AS TYPE_SERV_CODE,
        forInsert.TYPE_SERV_NAME    AS TYPE_SERV_NAME,
        forInsert.COUNT_SERV_NORMAL AS COUNT_SERV_NORMAL,
        forInsert.COUNT_SERV_OVER   AS COUNT_SERV_OVER,
        socServAGR.A_ID             AS SOC_SERV_AGR_OUID,
        condition.A_COND_SOC_SERV
    FROM ESRN_SOC_SERV socServ --Назначение социального обслуживания.
    ----Агрегация по социальной услуге.
        INNER JOIN WM_SOC_SERV_AGR socServAGR 
            ON socServAGR.ESRN_SOC_SERV = socServ.OUID 
                AND socServAGR.A_STATUS = 10 --Статус агрегации в БД "Действует".
    ----Тарифы на социальные услуги.    
        INNER JOIN SPR_TARIF_SOC_SERV socServTarif
            ON socServTarif.A_ID = socServAGR.A_SOC_SERV
                AND socServTarif.A_STATUS = 10 --Статус тарифа в БД "Действует".
    ----Социальные услуги.
        INNER JOIN SPR_SOC_SERV typeSocServ
            ON typeSocServ.OUID = socServTarif.A_SOC_SERV
                AND typeSocServ.A_STATUS = 10 --Статус социальной услуги в БД "Действует".
    ----Данные для вставки.
        INNER JOIN #DATA_FOR_INSERV forInsert
            ON CHARINDEX(forInsert.TYPE_SERV_CODE, typeSocServ.A_CODE) > 0 
            --AND forInsert.TYPE_SERV_NAME = typeSocServ.A_NAME
       INNER JOIN WM_COND_SOC_SERV_ONE condition
            ON condition.A_SOC_SERV_AGR = socServAGR.A_ID --Агрегация отчета. 
                AND condition.A_STATUS = 10
                AND (@yearReport_INT BETWEEN YEAR(A_STARTDATE) AND ISNULL(YEAR(A_LASTDATE), 9999) --Год отчета входит в период действия назначения.
                AND (@yearReport_INT <> YEAR(A_STARTDATE) AND @yearReport_INT <> ISNULL(YEAR(A_LASTDATE), 9999) --Год отчета не равен крайнему.
                    OR @yearReport_INT = YEAR(A_STARTDATE) AND @monthReport_INT >= MONTH(A_STARTDATE) --Или равен начальному, но месяц позже начала.
                    OR @yearReport_INT = YEAR(A_LASTDATE) AND @monthReport_INT <= MONTH(A_LASTDATE) --Или равен конечному, но месяц раньше конца.
                )
            )
    WHERE socServ.A_STATUS = 10 --Статус назначения в БД "Действует".
        AND socServ.OUID = @identifiedSocServ --Идентифицированное назначение.
    --Подсчет услуг.
    DECLARE @countTypeServForInsert INT
    SET @countTypeServForInsert = (SELECT COUNT(*) FROM #DATA_FOR_INSERV)
    DECLARE @countFoundTypeServ INT
    SET @countFoundTypeServ = (SELECT COUNT(*) FROM #FOUND_AGREGATION)
    --Результат 4 этапа.
    IF (@countTypeServForInsert <> @countFoundTypeServ) BEGIN 
        SET @thereIsError = 1
        SET @message = 'Не все услуги были найдены в социальном обслуживании'
    END
END
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--Этап 4: Проверка введенных данных.
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
IF (@thereIsError = 0) BEGIN
    --Проверка отсутствия уже введенных данных.
    DECLARE @alreadyThereCount INT
    SET @alreadyThereCount = (
        SELECT 
            COUNT(DISTINCT cosSocServ.A_ID)
        FROM #FOUND_AGREGATION foundAgregation
            INNER JOIN WM_COST_SOC_SERV cosSocServ
                ON cosSocServ.A_AGR_SOC_SERV = foundAgregation.SOC_SERV_AGR_OUID
                    AND cosSocServ.A_STATUS = 10
                    AND cosSocServ.A_DATE_START = @startDate
                    AND cosSocServ.A_DATE_LAST = @endDate
        )
    IF (@alreadyThereCount = @countTypeServForInsert) BEGIN 
        SET @thereIsError = 1
        SET @message = 'Данные уже есть за данный период'
    END 
    IF (@alreadyThereCount <> @countTypeServForInsert AND @alreadyThereCount <> 0) BEGIN 
        SET @thereIsError = 1
        SET @message = 'Частично данные уже есть за данный период'
    END     
END 
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--Этап 5: Вставка стоимости и количество оказанных социальных услуг.
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
IF (@thereIsError = 0) BEGIN
    INSERT INTO WM_COST_SOC_SERV(A_EMPLOYEE, A_EDITOWNER, A_STATUS, GUID, TS, SYSTEMCLASS, A_CREATEDATE, A_CROWNER, A_AGR_SOC_SERV, A_SUM_SOC_SERV_PERIOD, A_DATE_START, A_DATE_LAST, A_ACT_VOLUME, A_COMMENT, A_ACT_EXCESS_QUANT, A_COST_DOP_SOC_SERV, A_ACT_QUANT_NORM)
    SELECT
        CAST(NULL AS INT)       AS A_EMPLOYEE,              --Сотрудники.
        CAST(NULL AS INT)       AS A_EDITOWNER,             --Изменил.
        10                      AS A_STATUS,                --Статус.
        NEWID()                 AS GUID,                    --Глобальный идентификатор.
        GETDATE()               AS TS,                      --Дата модификации.
        10284209                AS SYSTEMCLASS,             --Класс объекта.
        GETDATE()               AS A_CREATEDATE,            --Дата создания.
        10314303                AS A_CROWNER,               --Автор. 
        SOC_SERV_AGR_OUID       AS A_AGR_SOC_SERV,          --Социальная услуга (Агрегация по социальной услуге).
        CAST(NULL AS FLOAT)     AS A_SUM_SOC_SERV_PERIOD,   --Сумма по услуге за период, руб.
        @startDate              AS A_DATE_START,            --Дата начала периода.
        @endDate                AS A_DATE_LAST,             --Дата окончания периода.
        CAST(NULL AS FLOAT)     AS A_ACT_VOLUME,            --Фактический объем работ на 1 услугу.
        CAST(NULL AS VARCHAR)   AS A_COMMENT,               --Примечание.
        COUNT_SERV_OVER         AS A_ACT_EXCESS_QUANT,      --Количество оказанных услуг в месяц сверх норматива.
        CAST(NULL AS FLOAT)     AS A_COST_DOP_SOC_SERV,     --Стоимость оказанных услуг, руб.
        COUNT_SERV_NORMAL       AS A_ACT_QUANT_NORM         --Количество оказанных услуг в месяц в рамках норматива.
    FROM #FOUND_AGREGATION foundAgregation
END 
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--Этап 6: Вставка суммы по услуге за календарный месяц.
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
IF (@thereIsError = 0) BEGIN
    INSERT INTO WM_COST_SOC_SERV_MONTH(A_YEAR, A_MONTH, A_SOC_SERV_MONTH, A_SUM_SOC_SERV_MONTH, A_NORM_EX, A_EDITOWNER, A_TS, A_GUID, A_STATUS, A_CREATEDATE, A_CROWNER, A_AGR_SOC_SERV, A_SYSTEMCLASS, A_FULL_COST, A_PERCENT_PART_PAY, A_SUM_NORM_EX, A_IS_PART_PAY, A_COND_SOC_SERV, A_NORMSOCSERV, A_ACT_EXCESS_QUANT)
    SELECT
        @yearReport_INT                     AS A_YEAR,              --Год.
        @monthReport_INT                    AS A_MONTH,             --Месяц.
        COUNT_SERV_NORMAL + COUNT_SERV_OVER AS A_SOC_SERV_MONTH,    --Количество оказанных услуг за месяц (Сумма норматива и превышения норматива).
        0                                   AS A_SUM_SOC_SERV_MONTH,
        CASE COUNT_SERV_OVER
            WHEN 0 THEN 0
            ELSE 1
        END                                 AS A_NORM_EX,           --Норматив превышен (Если есть сумма превышения норматива).
        CAST(NULL AS INT)                   AS A_EDITOWNER,         --Изменил.        
        GETDATE()                           AS A_TS,                --Дата модификации.
        NEWID()                             AS A_GUID,              --Глобальный идентификатор.
        10                                  AS A_STATUS,            --Статус.
        GETDATE()                           AS A_CREATEDATE,        --Дата создания.
        10314303                            AS A_CROWNER,           --Автор.
        SOC_SERV_AGR_OUID                   AS A_AGR_SOC_SERV,      --Социальная услуга (Агрегация социальной услуги)
        10284209                            AS A_SYSTEMCLASS,       --Класс объекта.
        --CASE @condition
        --    WHEN 'free' THEN @costServOver
        --    WHEN 'part' THEN @costServNormal / 50 + @costServOver
        --    WHEN 'full' THEN @costServNormal + @costServOver
        --END                                 
        0                                   AS A_FULL_COST,         --Полная стоимость оказанных услуг, руб.
        CASE CONDITION
            WHEN 'part' THEN 50
            ELSE 100
        END                                 AS A_PERCENT_PART_PAY,  --Размер частичной оплаты услуг, %
        0                                   AS A_SUM_NORM_EX,       --Сумма превышения норматива, руб.
        0                                   AS A_IS_PART_PAY,       --Без учета частичной оплаты
        CONDITION                           AS A_COND_SOC_SERV,     --Условие оказания социальных услуг.
        COUNT_SERV_NORMAL                   AS A_NORMSOCSERV,       --Количество оказанных услуг в рамках норматива.
        COUNT_SERV_OVER                     AS A_ACT_EXCESS_QUANT   --Количество оказанных услуг в месяц сверх норматива.
    FROM #FOUND_AGREGATION
END  
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--Этап 7: Вставка стоимости всех оказанных услуг за календарный месяц 
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
IF (@thereIsError = 0) BEGIN
    --Условия оказания социальных услуг.
    DECLARE @condition VARCHAR(256) SET @condition = (
        SELECT 
            A_COND_SOC_SERV 
        FROM WM_COND_SOC_SERV WHERE A_SOC_SERV = @identifiedSocServ --Назначение       
            AND (@yearReport_INT BETWEEN YEAR(A_STARTDATE) AND ISNULL(YEAR(A_LASTDATE), 9999) --Год отчета входит в период действия назначения.
                AND (@yearReport_INT <> YEAR(A_STARTDATE) AND @yearReport_INT <> ISNULL(YEAR(A_LASTDATE), 9999) --Год отчета не равен крайнему.
                    OR @yearReport_INT = YEAR(A_STARTDATE) AND @monthReport_INT >= MONTH(A_STARTDATE) --Или равен начальному, но месяц позже начала.
                    OR @yearReport_INT = YEAR(A_LASTDATE) AND @monthReport_INT <= MONTH(A_LASTDATE) --Или равен конечному, но месяц раньше конца.
                )
        )
    )
    INSERT INTO WM_FACT_COST_SOC_SERV(TS,SYSTEMCLASS,GUID,A_CREATEDATE,A_CROWNER,A_SERV,A_SUM_PERIOD,A_FACT_PAY,A_EDITOWNER,A_STATUS,A_PAY_DATE,A_YEAR,A_MONTH,A_FULL_COST_MONTH,A_SUM_PAY,A_PART_PAY,A_SUMM_EXT_SERV,A_SUM_NORM_EX,A_SUMM_DOG,ESRN_SOC_SERV,A_COND_SOC_SERV,A_COMMENT,A_SUM_PERIOD_BUDGET,A_FULL_COUNT,A_SOC_COUNT,A_SOC_COST_MONTH,A_DOP_COUNT,A_DOP_COST_MONTH,A_OTHER_COUNT,A_OTHER_COST_MONTH)
    SELECT
        GETDATE()                   AS TS,                  --Дата модификации
        10284266                    AS SYSTEMCLASS,         --Класс объекта
        NEWID()                     AS GUID,                --Глобальный идентификатор
        GETDATE()                   AS A_CREATEDATE,        --Дата создания
        10314303                    AS A_CROWNER,           --Автор
        CAST(NULL AS INT)           AS A_SERV,              --Назначение
        0                           AS A_SUM_PERIOD,        --Всего к оплате гражданином, руб.
        'withoutPay'                AS A_FACT_PAY,          --Статус оплаты
        CAST(NULL AS INT)           AS A_EDITOWNER,         --Изменил
        10                          AS A_STATUS,            --Статус
        CAST(NULL AS DATE)          AS A_PAY_DATE,          --Дата оплаты
        @yearReport_INT                 AS A_YEAR,              --Год
        @monthReport_INT                AS A_MONTH,             --Месяц
        0                           AS A_FULL_COST_MONTH,   --Полная стоимость оказанных услуг, руб.
        0                           AS A_SUM_PAY,           --Оплачено гражданином, руб.
        0                           AS A_PART_PAY,          --Остаток к оплате, руб.
        0                           AS A_SUMM_EXT_SERV,     --Сумма по дополнительным услугам, руб.
        0                           AS A_SUM_NORM_EX,       --Сумма превышения норматива, руб.
        CAST(NULL AS FLOAT)         AS A_SUMM_DOG,          --Размер частичной оплаты по договору, руб.
        @identifiedSocServ          AS ESRN_SOC_SERV,       --Назначение на социальное обслуживание
        @condition                  AS A_COND_SOC_SERV,     --Условие оказания социальных услуг
        CAST(NULL AS VARCHAR)       AS A_COMMENT,           --Комментарий
        0                           AS A_SUM_PERIOD_BUDGET, --Всего к оплате из средств бюджета
        SUM(t.TOTAL_COUNT_NORMAL)   AS A_FULL_COUNT,        --Количество оказанных услуг
        SUM(t.TOTAL_COUNT_NORMAL)   AS A_SOC_COUNT,         --Количество оказанных соц. услуг
        0                           AS A_SOC_COST_MONTH,    --Полная стоимость оказанных соц. услуг, руб.
        CAST(NULL AS INT)           AS A_DOP_COUNT,         --Количество оказанных доп. услуг
        CAST(NULL AS FLOAT)         AS A_DOP_COST_MONTH,    --Полная стоимость оказанных доп. услуг, руб.
        CAST(NULL AS INT)           AS A_OTHER_COUNT,       --Количество оказанных иных услуг
        CAST(NULL AS FLOAT)         AS A_OTHER_COST_MONTH   --Полная стоимость оказанных иных услуг, руб.
    FROM (
        SELECT 
            socServAGR.A_ID                     AS SOC_SERV_AGR_OUID,
            SUM(costSocServMonth.A_NORMSOCSERV) AS TOTAL_COUNT_NORMAL
        FROM ESRN_SOC_SERV socServ --Назначение социального обслуживания.
        ----Агрегация по социальной услуге.
            INNER JOIN WM_SOC_SERV_AGR socServAGR 
                ON socServAGR.ESRN_SOC_SERV = socServ.OUID 
                    AND socServAGR.A_STATUS = 10 --Статус агрегации в БД "Действует".
        ----Cуммы по услуге за календарный месяц.
            INNER JOIN WM_COST_SOC_SERV_MONTH costSocServMonth
                ON costSocServMonth.A_AGR_SOC_SERV = socServAGR.A_ID
                    AND costSocServMonth.A_STATUS = 10
                    AND costSocServMonth.A_YEAR = @yearReport_INT
                    AND costSocServMonth.A_MONTH = @monthReport_INT
        WHERE socServ.A_STATUS = 10 --Статус назначения в БД "Действует".
            AND socServ.OUID = @identifiedSocServ --Требуемое назначение.
        GROUP BY socServAGR.A_ID
    ) t
END
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--Этап 8: Завершение
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--Вывод результата.
SELECT @thereIsError AS thereIsError, @message AS message
--Удаление временных таблиц.
IF OBJECT_ID('tempdb..#FOUND_PEOPLE') IS NOT NULL BEGIN DROP TABLE #FOUND_PEOPLE END --Найденные люди по входным данным.
IF OBJECT_ID('tempdb..#FOUND_SOC_SERV') IS NOT NULL BEGIN DROP TABLE #FOUND_SOC_SERV END --Найденные социальные обслуживания по входным данным.
IF OBJECT_ID('tempdb..#DATA_FOR_INSERV') IS NOT NULL BEGIN DROP TABLE #DATA_FOR_INSERV END --Данные для вставки.