-- SELECT 1 FROM OJDT T0 WHERE T0.DueDate >= [%0] AND T0.DueDate <=[%1]

DECLARE @dtin DATE, @dtfi DATE

--SET @dtin = '[%0]'
--SET @dtfi = '[%1]'
--SET @dtin = '20160101'
--SET @dtfi = '20180930'

	SELECT DISTINCT 
	REPLACE(REPLACE(REPLACE ( T9.cnpjFornecedor , '.' , '' ), '/',''),'-','') as id,
	t9.type id_type,
	T2.[CardName] legal_name, 
	t2.E_Mail as email,
	t2.Phone1 + ' - ' + t2.Phone2 + ' - ' +t2.Cellular as phone,
	t2.CardCode,
	T1.[BalDueCred]  - ISNULL(
	(SELECT SUM(TT7.WTAMNT/ISNULL(TT8.INSTNUM,1))
		 FROM JDT1 TT1
		 INNER JOIN OCRD TT2 ON TT1.ShortName = TT2.CardCode
		 LEFT JOIN VPM2 TT3 ON TT1.TransId = TT3.DocNum AND TT3.InvType = 18 
		 INNER JOIN OPCH TT6 ON TT1.SOURCEID = TT6.DOCNUM AND ( TT1.[TransType] <> 204 and TT1.[TransType] <> 30) 
		 INNER JOIN PCH5 TT7 ON TT7.ABSENTRY = TT6.DOCENTRY AND ( TT1.[TransType] <> 204 and TT1.[TransType] <> 30) 
		 INNER JOIN OCTG TT8 ON TT8.GROUPNUM = TT6.GROUPNUM  AND ( TT1.[TransType] <> 204 and TT1.[TransType] <> 30) 
		 WHERE Substring(TT1.[ShortName],1,1) = 'F'
		 AND (TT1.[TransType] = 30 or TT1.[TransType]=18 or TT1.[TransType]=204)
		 AND SUBSTRING (TT1.[Account] ,1,1)= '2'
		 AND TT1.[Credit]> 0
		 AND TT1.BALDUECRED <>0 
		 AND TT1.[ShortName] = T1.[ShortName]
		 AND TT1.[TransType] = T1.[TransType] 
		 AND  TT1.[TransId]= T1.[TransId]
		 AND TT7.[Category] = 'P'
		 --AND TT1.[DueDate]>= @dtin and TT1.[DueDate]<= @dtfi
	)/(ISNULL(T8.INSTNUM,1)),0)  as value,
	T1.[Credit] as original_value,
	substring(CONVERT(varchar,T1.[DueDate],126),0,11) AS due_date,
	substring(CONVERT(varchar,t1.refdate ,126),0,11) as issue_date,
	T6.Serial as tax_doc_id,
	t6.SeriesStr as tax_doc_sec_id,
	isnull(t6.U_chaveacesso,'') as tax_doc_key,
	[Ref3Line] installment,
	(select top 1 isnull(c1.ISOCurrCod,(select top 1 z1.ISOCurrCod from OADM z0 left join OCRN z1 on z0.SysCurrncy = z1.CurrCode))
		from JDT1 C0 left join OCRN C1 on c0.FCCurrency = c1.CurrCode ) as currency,
	t1.TransId,
	t1.Line_ID,
	case when (t1.U_lb_release = 1) then 'true' else 'false' end as release
	--'lastchange' as lastchange 

	FROM JDT1 T1
	INNER JOIN OCRD T2 ON T1.ShortName = T2.CardCode
	LEFT JOIN VPM2 T3 ON T1.TransId = T3.DocNum AND (T3.InvType <> 30 and T3.InvType <> 18  and T3.InvType <> 204 )
	LEFT JOIN OPCH T6 ON T1.SOURCEID = T6.DocEntry AND ( T1.[TransType] <> 204 and T1.[TransType] <> 30)
	LEFT JOIN PCH5 T7 ON T7.ABSENTRY = T6.DOCENTRY AND ( T1.[TransType] <> 204 and T1.[TransType] <> 30) 
	LEFT JOIN OCTG T8 ON T8.GROUPNUM = T6.GROUPNUM  AND ( T1.[TransType] <> 204 and T1.[TransType] <> 30) 
	LEFT JOIN (SELECT max( ISNULL (T0.TaxId0 , T0.TaxId4) ) as cnpjFornecedor ,T0.CARDCODE, case when T0.TaxId0 is not null then 'CNPJ' else 'CPF' end as type
				FROM CRD7 T0
				where t0.CardCode like 'F%' and (T0.TaxId0 is not null or T0.TaxId4 is not null)
				GROUP BY T0.CARDCODE, T0.TaxId0) as T9 ON T9.CARDCODE = T2.CardCode

	WHERE 
	--(T9.cnpjFornecedor IS NOT NULL AND T9.cnpjFornecedor !='') AND 
	Substring(T1.[ShortName],1,1) = 'F'
	AND T1.[TransType]=18
	AND SUBSTRING (T1.[Account] ,1,1)= '2' 
	AND T1.[Credit]> 0
	AND T1.BALDUECRED <>0  
	--AND T1.[DueDate] between @dtin AND @dtfi
	--and t1.TransId = 221672

	--ORDER BY emailContatoFornecedor
	--ORDER BY DueDater
