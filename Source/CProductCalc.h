#pragma once

#include "CFactory.h"
#include "CSmartArray.h"
#include "ProductTag.h"
#include "CProductCallFill.h"
class CProductCalc
{
	CSmartArray m_Array;
	CSmartArray m_AlreadyHave;
	CAtlArray<CFactory*> m_FactArray;
public:
	CProductCalc()
	{
		m_FactArray.Add(new CMilkFactory());
		m_FactArray.Add(new CBakery());
		m_FactArray.Add(new СWeavingFactory());
		m_FactArray.Add(new CSewingFactory());
		m_FactArray.Add(new CSnackFactory());
		m_FactArray.Add(new CFastFoodFactory());
		m_FactArray.Add(new CRubberFactory());
		m_FactArray.Add(new CSugarFactory());
		m_FactArray.Add(new CPaperFactory());
		m_FactArray.Add(new CIceCreamFactory());
		m_FactArray.Add(new СconfectioneryFactory());

		SetHaveProduct(m_AlreadyHave);
	}

	~CProductCalc()
	{
		size_t count = m_FactArray.GetCount();
		for (size_t i = 0; i < count; i++)
		{
			delete m_FactArray[i];
		}
	}
	void Add(CString name, int count)
	{
		m_Array.Add(name, count);
	}
	void Calc()
	{
		size_t count = m_FactArray.GetCount();
		m_Array.ExcludeWhatHave(m_AlreadyHave);
		//if (firstcalc)
		//{

		//}
		for (size_t i = 0; i < count; i++)
		{
			CFactory* pFact = m_FactArray[i];
			pFact->SetInput(m_Array);
			pFact->Calc();
			pFact->UpdateResult(m_Array);
			m_Array.ExcludeWhatHave(m_AlreadyHave);
		}
	}

	void Print()
	{
		OutputDebugString(_T("Информация по фабрикам:\n\r"));
		size_t count = m_FactArray.GetCount();
		for (int pr = 0; pr < (int)PriorityTag::three + 1; pr++)
		{
			for (size_t i = 0; i < count; i++)
			{
				if (m_FactArray[i]->GetPriorety() == pr)
					m_FactArray[i]->Print();
			}

		}
		OutputDebugString(_T("\n\rИнформация по ингридиентам:\n\r"));
		m_Array.Print();
	}
};