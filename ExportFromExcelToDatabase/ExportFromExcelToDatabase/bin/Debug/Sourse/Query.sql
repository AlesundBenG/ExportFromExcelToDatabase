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