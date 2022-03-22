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
DECLARE @organization VARCHAR(256) SET @organization = '#organization#'
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
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--Этап 2: Сохранение параметров.
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
INSERT INTO TEMPORARY_TABLE (VARCHAR_1, VARCHAR_2, VARCHAR_3, VARCHAR_4, VARCHAR_5, VARCHAR_6, VARCHAR_7, VARCHAR_8, VARCHAR_9, VARCHAR_10, VARCHAR_11, VARCHAR_12, VARCHAR_13, VARCHAR_14, VARCHAR_15)
SELECT
    GETDATE()                           AS VARCHAR_1,
    @organization                       AS VARCHAR_2,
    @yearReport                         AS VARCHAR_3,
    @monthReport                        AS VARCHAR_4,
    @SNILS                              AS VARCHAR_5,
    @surname                            AS VARCHAR_6,
    @name                               AS VARCHAR_7,
    @secondname                         AS VARCHAR_8,
    @birthdate                          AS VARCHAR_9,
    @formSocServ                        AS VARCHAR_10,
    TYPE_SERV_CODE                      AS VARCHAR_11,
    TYPE_SERV_NAME                      AS VARCHAR_12,
    CONVERT(VARCHAR, COUNT_SERV_NORMAL) AS VARCHAR_13,
    CONVERT(VARCHAR, COUNT_SERV_OVER)   AS VARCHAR_14,
    'Реестр по услугам'                 AS VARCHAR_15
FROM #DATA_FOR_INSERV
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--Этап 3: Завершение
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--Вывод результата.
SELECT 0 AS thereIsError, 'Записано' AS message
--Удаление временных таблиц.
IF OBJECT_ID('tempdb..#FOUND_PEOPLE') IS NOT NULL BEGIN DROP TABLE #FOUND_PEOPLE END --Найденные люди по входным данным.
IF OBJECT_ID('tempdb..#FOUND_SOC_SERV') IS NOT NULL BEGIN DROP TABLE #FOUND_SOC_SERV END --Найденные социальные обслуживания по входным данным.
IF OBJECT_ID('tempdb..#DATA_FOR_INSERV') IS NOT NULL BEGIN DROP TABLE #DATA_FOR_INSERV END; --Данные для вставки.