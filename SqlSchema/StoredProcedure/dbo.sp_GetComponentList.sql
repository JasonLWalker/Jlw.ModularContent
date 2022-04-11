SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Jason L Walker
-- Create date: 2020-10-01
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetComponentList] 
	-- Add the parameters for the stored procedure here
    @groupKey varchar(40),
    @language varchar(5) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
    DECLARE @tableName varchar(40) = 'ImmediateTransferRequests';

    -- table variable to hold schema. This mimics the INFORMATION_SCHEMA.COLUMNS view
    -- Structure info taken from: https://docs.microsoft.com/en-us/sql/relational-databases/system-information-schema-views/columns-transact-sql?view=sql-server-ver15
    DECLARE @controlValues TABLE(
        TABLE_CATALOG	nvarchar(128)	-- Table qualifier.
        ,TABLE_SCHEMA	nvarchar(128)	-- Name of schema that contains the table.
                                        -- ** Important ** Do not use INFORMATION_SCHEMA views to determine the schema of an object. INFORMATION_SCHEMA views only represent a subset of the metadata of an object. The only reliable way to find the schema of a object is to query the sys.objects catalog view.
        ,TABLE_NAME	nvarchar(128)       -- Table name.
        ,COLUMN_NAME	nvarchar(128)   -- Column name.
        ,ORDINAL_POSITION int           -- Column identification number.
        ,COLUMN_DEFAULT	nvarchar(4000)  -- Default value of the column.
        ,IS_NULLABLE	varchar(3)      -- Nullability of the column. If this column allows for NULL, this column returns YES. Otherwise, NO is returned.
        ,DATA_TYPE	nvarchar(128)       -- System-supplied data type.
        ,CHARACTER_MAXIMUM_LENGTH int   -- Maximum length, in characters, for binary data, character data, or text and image data. -1 for xml and large-value type data. Otherwise, NULL is returned. For more information, see Data Types (Transact-SQL).
        ,CHARACTER_OCTET_LENGTH	int     -- Maximum length, in bytes, for binary data, character data, or text and image data. -1 for xml and large-value type data. Otherwise, NULL is returned.
        ,NUMERIC_PRECISION	tinyint     -- Precision of approximate numeric data, exact numeric data, integer data, or monetary data. Otherwise, NULL is returned.
        ,NUMERIC_PRECISION_RADIX smallint   -- Precision radix of approximate numeric data, exact numeric data, integer data, or monetary data. Otherwise, NULL is returned.
        ,NUMERIC_SCALE	int	                -- Scale of approximate numeric data, exact numeric data, integer data, or monetary data. Otherwise, NULL is returned.
        ,DATETIME_PRECISION	smallint        -- Subtype code for datetime and ISO interval data types. For other data types, NULL is returned.
        ,CHARACTER_SET_CATALOG nvarchar(128)   -- Returns master. This indicates the database in which the character set is located, if the column is character data or text data type. Otherwise, NULL is returned.
        ,CHARACTER_SET_SCHEMA nvarchar(128)   -- Always returns NULL.
        ,CHARACTER_SET_NAME	nvarchar(128)       -- Returns the unique name for the character set if this column is character data or text data type. Otherwise, NULL is returned.
        ,COLLATION_CATALOG	nvarchar(128)       -- Always returns NULL.
        ,COLLATION_SCHEMA	nvarchar(128)       -- Always returns NULL.
        ,COLLATION_NAME	nvarchar(128)           -- Returns the unique name for the collation if the column is character data or text data type. Otherwise, NULL is returned.
        ,DOMAIN_CATALOG	nvarchar(128)           -- If the column is an alias data type, this column is the database name in which the user-defined data type was created. Otherwise, NULL is returned.
        ,DOMAIN_SCHEMA	nvarchar(128)           -- If the column is a user-defined data type, this column returns the name of the schema of the user-defined data type. Otherwise, NULL is returned.
                                                -- ** Important ** Do not use INFORMATION_SCHEMA views to determine the schema of a data type. The only reliable way to find the schema of a type is to use the TYPEPROPERTY function.
        ,DOMAIN_NAME	nvarchar(128)           -- If the column is a user-defined data type, this column is the name of the user-defined data type. Otherwise, NULL is returned.         
        
        ,LABEL nvarchar(256)                    -- Extra Value not in TSQL, but used by this system
    );

    DECLARE @count int = 0;

IF (@groupKey = 'jlwNativeHtmlControls') 
BEGIN
    SET @tableName = @groupKey;
    --SET @count = @count + 1;
    INSERT INTO 
        @controlValues 
        (TABLE_NAME, COLUMN_NAME, LABEL, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, ORDINAL_POSITION) 
    VALUES 
        (@tableName, 'Button' , 'Button', 'button', null, 0 )
        ,(@tableName, 'EmbedForm' , 'Embeded Form', 'EMBED', null, 0 )
        ,(@tableName, 'Form' , 'Form', 'FORM', null, 0 )
        ,(@tableName, 'HtmlBlock' , 'HTML Text Block. Lorem ipsum dolor sit amet, consectetur adipiscing elit.', 'HTML', null, 0 )
        ,(@tableName, 'Separator' , 'Separator', 'SEPARATOR', null, 0 )
        ,(@tableName, 'DropDownSelect' , 'Form Drop-down', 'SELECT', null, 0 )
        ,(@tableName, 'TextArea' , 'Multi-line Form Input', 'TEXTAREA', null, 0 )

        ,(@tableName, 'DateInput' , 'Date Input', 'date', null, 0 )
        ,(@tableName, 'DateTimeInput' , 'Date/Time Input', 'datetime', null, 0 )
        ,(@tableName, 'MonthInput' , 'Month Input', 'month', null, 0 )
        ,(@tableName, 'TimeInput' , 'Time Input', 'time', null, 0 )
        ,(@tableName, 'WeekInput' , 'Week Input', 'week', null, 0 )
        
        ,(@tableName, 'ColorInput' , 'Color Input', 'color', null, 0 )
        ,(@tableName, 'CheckbboxInput' , 'Checkbox Input', 'bit', null, 0 )
        ,(@tableName, 'HiddenInput' , 'Hidden Input', 'hidden', null, 0 )
        ,(@tableName, 'RadioInput' , 'Radio Button Input', 'radio', null, 0 )
        ,(@tableName, 'SliderInput' , 'Slider Input', 'range', null, 0 )
        
        ,(@tableName, 'TextInput' , 'Text Input', 'varchar', 40, 0 )
        ,(@tableName, 'PasswordInput' , 'Password Input', 'password', 40, 0 )
        ,(@tableName, 'PhoneInput' , 'Phone Input', 'phone', 20, 0 )
        ,(@tableName, 'UrlInput' , 'Url Input', 'url', 100, 0 )
        ,(@tableName, 'EmailInput' , 'Email Input', 'email', 100, 0 )
        ,(@tableName, 'NumberInput' , 'Number Input', 'int', 40, 0 )
        ,(@tableName, 'SearchInput' , 'Search Input', 'search', 40, 0 )
END
ELSE
BEGIN
    INSERT INTO @controlValues
    SELECT 
        TABLE_CATALOG
        ,TABLE_SCHEMA
        ,TABLE_NAME
        ,COLUMN_NAME
        ,ORDINAL_POSITION
        ,COLUMN_DEFAULT
        ,IS_NULLABLE
        ,DATA_TYPE
        ,CHARACTER_MAXIMUM_LENGTH
        ,CHARACTER_OCTET_LENGTH
        ,NUMERIC_PRECISION
        ,NUMERIC_PRECISION_RADIX
        ,NUMERIC_SCALE
        ,DATETIME_PRECISION
        ,CHARACTER_SET_CATALOG
        ,CHARACTER_SET_SCHEMA
        ,CHARACTER_SET_NAME
        ,COLLATION_CATALOG
        ,COLLATION_SCHEMA
        ,COLLATION_NAME
        ,DOMAIN_CATALOG
        ,DOMAIN_SCHEMA
        ,DOMAIN_NAME        
        ,COLUMN_NAME AS LABEL -- not in TSQ, so use column name
    FROM 
        INFORMATION_SCHEMA.COLUMNS
    WHERE
        TABLE_NAME = @tableName
END

    SELECT 
        LABEL AS [Label],
        @groupKey AS [GroupKey],
        COLUMN_NAME AS FieldKey,         
        CASE 
            WHEN DATA_TYPE = 'bigint' THEN 'INPUT'
            WHEN DATA_TYPE = 'bit' THEN 'INPUT'
            WHEN DATA_TYPE = 'int' THEN 'INPUT'
            WHEN DATA_TYPE = 'char' THEN 'INPUT'
            WHEN DATA_TYPE = 'date' THEN 'INPUT'
            WHEN DATA_TYPE = 'datetime' THEN 'INPUT'
            WHEN DATA_TYPE = 'varchar' AND CHARACTER_MAXIMUM_LENGTH = -1 THEN 'TEXTAREA'
            WHEN DATA_TYPE = 'varchar' AND CHARACTER_MAXIMUM_LENGTH != -1 THEN 'INPUT'
            WHEN DATA_TYPE = 'nvarchar' AND CHARACTER_MAXIMUM_LENGTH = -1 THEN 'TEXTAREA'
            WHEN DATA_TYPE = 'nvarchar' AND CHARACTER_MAXIMUM_LENGTH = -1 THEN 'INPUT'

            WHEN DATA_TYPE = 'button' THEN 'BUTTON'
            WHEN DATA_TYPE = 'embed' THEN 'EMBED'
            WHEN DATA_TYPE = 'form' THEN 'FORM'
            WHEN DATA_TYPE = 'html' THEN 'HTML'
            WHEN DATA_TYPE = 'body' THEN 'BODY'
            WHEN DATA_TYPE = 'heading' THEN 'HEADING'
            WHEN DATA_TYPE = 'separator' THEN 'SEPARATOR'
            WHEN DATA_TYPE = 'select' THEN 'SELECT'
            WHEN DATA_TYPE = 'textarea' THEN 'TEXTAREA'
            WHEN DATA_TYPE = 'wizard' THEN 'WIZARD'
            WHEN DATA_TYPE = 'screen' THEN 'SCREEN'

            ELSE 'INPUT'
        END AS FieldType,
        CASE
            WHEN DATA_TYPE = 'bit' THEN 'mr-1'
            WHEN DATA_TYPE = 'radio' THEN 'mr-1'
            WHEN DATA_TYPE = 'button' THEN 'btn btn-primary btn-sm w-100'
            WHEN DATA_TYPE = 'embed' THEN ''
            WHEN DATA_TYPE = 'form' THEN ''
            WHEN DATA_TYPE = 'html' THEN ''
            --WHEN DATA_TYPE = 'body' THEN ''
            --WHEN DATA_TYPE = 'heading' THEN ''
            WHEN DATA_TYPE = 'separator' THEN ''
            --WHEN DATA_TYPE = 'select' THEN ''
            --WHEN DATA_TYPE = 'textarea' THEN ''
            WHEN DATA_TYPE = 'wizard' THEN ''
            WHEN DATA_TYPE = 'screen' THEN ''

            ELSE 'form-control form-control-sm' 
        END AS FieldClass, 
        'col-12' as WrapperClass,
        REPLACE(REPLACE(( 
        SELECT  
            TOP 1 
            --*  
            CASE 
                WHEN DATA_TYPE='bit' THEN 'checkbox'
                WHEN DATA_TYPE='bigint' THEN 'number'
                WHEN DATA_TYPE='date' THEN 'date'
                WHEN DATA_TYPE='datetime' THEN 'datetime-local'
                WHEN DATA_TYPE='time' THEN 'time'
                WHEN DATA_TYPE='month' THEN 'month'
                WHEN DATA_TYPE='week' THEN 'week'
                WHEN DATA_TYPE='int' THEN 'number'
                WHEN DATA_TYPE='varchar' AND CHARACTER_MAXIMUM_LENGTH = -1 THEN null
                WHEN DATA_TYPE='nvarchar' AND CHARACTER_MAXIMUM_LENGTH = -1 THEN null

                WHEN DATA_TYPE='radio' THEN 'radio'
                WHEN DATA_TYPE='color' THEN 'color'
                WHEN DATA_TYPE='hidden' THEN 'hidden'
                WHEN DATA_TYPE='range' THEN 'range'

                WHEN DATA_TYPE='password' THEN 'password'
                WHEN DATA_TYPE='phone' THEN 'phone'
                WHEN DATA_TYPE='email' THEN 'email'
                WHEN DATA_TYPE='url' THEN 'url'
                WHEN DATA_TYPE='search' THEN 'search'

                ELSE 'text'
            END AS [type],
            IIF(DATA_TYPE = 'form' OR DATA_TYPE = 'embed', 1, null) AS useCardLayout,
            IIF( CHARACTER_MAXIMUM_LENGTH != -1, CHARACTER_MAXIMUM_LENGTH, null ) AS [maxlength]
        FROM @controlValues j
        WHERE TABLE_NAME = @tableName AND t.COLUMN_NAME = j.COLUMN_NAME

        FOR JSON AUTO 
        ), '[', ''), ']', '') as FieldData
    FROM @controlValues t
    ORDER BY ORDINAL_POSITION


END


GO
