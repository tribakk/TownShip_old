#pragma once
#include "CSmartArray.h"

class CFactory
{
protected:
	CAtlArray<ProductTag> m_TagInputArray;
	CAtlArray<int> m_InputCount;
	CSmartArray  m_SmartInput;
	CSmartArray m_Result;
	PriorityTag m_Priority;
	void Init()
	{
		size_t count = m_TagInputArray.GetCount();
		for (size_t i = 0; i < count; i++)
		{
			m_InputCount.Add(0);
		}
	}
public:
	virtual void Calc() = 0;
	virtual CString GetFactName() = 0;
	void SetInput(CSmartArray& Array)
	{
		size_t count = m_TagInputArray.GetCount();
		for (size_t i = 0; i < count; i++)
		{
			ProductTag tag = m_TagInputArray[i];
			int ProductCount = Array.GetTagCount(tag);
			m_InputCount[i] = ProductCount;
			m_SmartInput.Add(tag, ProductCount);
			Array.ClearTag(tag);
		}
	}
	PriorityTag GetPriorety()
	{
		return m_Priority;
	}
	//void ClearSmartInput()
	//{
	//	m_SmartInput.RemoveAll();
	//}
	void UpdateResult(CSmartArray& arr)
	{
		arr.Merge(m_Result);
		m_Result.RemoveAll();
	}
	CSmartArray* GetResult()
	{
		return &m_Result;
	}

	void AddResult(CString name, int count)
	{
		m_Result.Add(spisok.GetTag(name), count);
	}
	void Print()
	{
		if (m_SmartInput.IsEmpty())
			return;
		CString pr;
		pr.Format(_T("\n\r") + GetFactName() + _T("\n\r"));
		OutputDebugString(pr);
		m_SmartInput.Print();
	}
};

class CMilkFactory : public CFactory
{
public:
	CMilkFactory()
	{
		m_TagInputArray.Add(spisok.GetTag(_T("сливки")));
		m_TagInputArray.Add(spisok.GetTag(_T("сыр")));
		m_TagInputArray.Add(spisok.GetTag(_T("масло")));
		m_TagInputArray.Add(spisok.GetTag(_T("йогурт")));
		m_Priority = PriorityTag::one;
		Init();
	}

	CString GetFactName()
	{
		return _T("Молочная фабрика");
	}
	void Calc()
	{
		int count = 0;
		int counter = 0;
		{
			//сливки
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("молоко")), 1 * count);
			counter++;
		}
		{
			//сливки
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("молоко")), 2 * count);
			counter++;
		}
		{
			//сливки
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("молоко")), 3 * count);
			counter++;
		}
		{
			//сливки
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("молоко")), 4 * count);
			counter++;
		}
	}

};

class CBakery : public CFactory
{
public:
	CBakery()
	{
		m_TagInputArray.Add(spisok.GetTag(_T("хлеб")));
		m_TagInputArray.Add(spisok.GetTag(_T("печенье")));
		m_TagInputArray.Add(spisok.GetTag(_T("бублик")));
		m_TagInputArray.Add(spisok.GetTag(_T("пицца")));
		m_TagInputArray.Add(spisok.GetTag(_T("картофельный хлеб")));
		m_TagInputArray.Add(spisok.GetTag(_T("банановый хлеб")));
		m_Priority = PriorityTag::one;
		Init();
	}
	CString GetFactName()
	{
		return _T("пекарня");
	}

	void Calc()
	{
		int count = 0;
		int counter = 0;
		{
			//хлеб
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("пшеница")), 2 * count);
			counter++;
		}
		{
			//печенье
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("пшеница")), 2 * count);
			m_Result.Add(spisok.GetTag(_T("яйцо")), 2 * count);
			counter++;
		}
		{
			//бублик
			m_Result.Add(spisok.GetTag(_T("пшеница")), 2 * count);
			m_Result.Add(spisok.GetTag(_T("яйцо")), 3 * count);
			m_Result.Add(spisok.GetTag(_T("сахар")), 3 * count);
			counter++;
		}
		{
			//пицца
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("пшеница")), 2 * count);
			m_Result.Add(spisok.GetTag(_T("сыр")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("томат")), 2 * count);
			counter++;
		}

		{
			//картофельный хлеб
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("пшеница")), 2 * count);
			m_Result.Add(spisok.GetTag(_T("картофель")), 2 * count);
			m_Result.Add(spisok.GetTag(_T("яйцо")), 4 * count);
			counter++;
		}

		{
			//банановый хлеб
			count = m_InputCount[5];
		}
	}

};

class СWeavingFactory : public CFactory
{
public:
	СWeavingFactory()
	{
		m_TagInputArray.Add(spisok.GetTag(_T("хлопковая ткань")));
		m_TagInputArray.Add(spisok.GetTag(_T("пряжа")));
		m_TagInputArray.Add(spisok.GetTag(_T("шелковая ткань")));
		m_Priority = PriorityTag::one;
		Init();
	}
	CString GetFactName()
	{
		return _T("Ткацкая фабрика");
	}
	void Calc()
	{
		int count = 0;
		int counter = 0;
		{
			//хлопковая ткань
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("хлопок")), 2 * count);
			counter++;
		}
		{
			//пряжа
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("шерсть")), 2 * count);
			counter++;
		}
		{
			//шелковая ткань
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("шелк")), 2 * count);
			counter++;
		}
	}

};

class CSewingFactory : public CFactory
{
public:
	CSewingFactory()
	{
		m_TagInputArray.Add(spisok.GetTag(_T("рубашка")));
		m_TagInputArray.Add(spisok.GetTag(_T("свитер")));
		m_TagInputArray.Add(spisok.GetTag(_T("пальто")));
		m_TagInputArray.Add(spisok.GetTag(_T("шляпа")));
		m_TagInputArray.Add(spisok.GetTag(_T("платье")));
		m_TagInputArray.Add(spisok.GetTag(_T("костюм")));
		m_Priority = PriorityTag::two;
		Init();
	}
	CString GetFactName()
	{
		return _T("Швейная фабрика");
	}

	void Calc()
	{
		int count = 0;
		int counter = 0;
		{
			//рубашка
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("хлопковая ткань")), 1 * count);
			counter++;
		}
		{
			//свитер
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("пряжа")), 1 * count);
			counter++;
		}
		{
			//пальто
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("хлопковая ткань")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("пряжа")), 1 * count);
			counter++;
		}
		{
			//шляпа
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("хлопковая ткань")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("шелковая ткань")), 1 * count);
			counter++;
		}
		{
			//платье
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("пряжа")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("шелковая ткань")), 1 * count);
			counter++;
		}
		{
			//костюм
			count = m_InputCount[counter];

			counter++;
		}
	}

};

class CSnackFactory : public CFactory
{
public:
	CSnackFactory()
	{
		m_TagInputArray.Add(spisok.GetTag(_T("попкорн")));
		m_TagInputArray.Add(spisok.GetTag(_T("кукурузные чипсы")));
		m_TagInputArray.Add(spisok.GetTag(_T("гранола")));
		m_TagInputArray.Add(spisok.GetTag(_T("чипсы")));
		m_TagInputArray.Add(spisok.GetTag(_T("канопе")));
		m_Priority = PriorityTag::three;
		Init();
	}
	CString GetFactName()
	{
		return _T("Фабрика закусок");
	}
	void Calc()
	{
		int count = 0;
		int counter = 0;
		{
			//попкорн
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("кукуруза")), 2 * count);
			counter++;
		}
		{
			//кукурузные чипсы
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("кукуруза")), 3 * count);
			counter++;
		}
		{
			//гранола
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("пшеница")), 2 * count);
			m_Result.Add(spisok.GetTag(_T("клубника")), 2 * count);
			counter++;
		}
		{
			//чипсы
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("картофель")), 2 * count);
			counter++;
		}
		{
			//канапе
			count = m_InputCount[counter];

			counter++;
		}
	}
};

class CFastFoodFactory : public CFactory
{
public:
	CFastFoodFactory()
	{
		m_TagInputArray.Add(spisok.GetTag(_T("милкшейк")));
		m_TagInputArray.Add(spisok.GetTag(_T("чизбургер")));
		m_TagInputArray.Add(spisok.GetTag(_T("сэндвич")));
		m_TagInputArray.Add(spisok.GetTag(_T("картошка фри")));
		m_TagInputArray.Add(spisok.GetTag(_T("печеный картофель")));
		m_Priority = PriorityTag::three;
		Init();
	}
	CString GetFactName()
	{
		return _T("Фабрика фастфуда");
	}
	void Calc()
	{
		int count = 0;
		int counter = 0;
		{
			//милкшейк
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("молоко")), 2 * count);
			m_Result.Add(spisok.GetTag(_T("клубника")), 1 * count);
			counter++;
		}
		{
			//чизбургер
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("хлеб")), 2 * count);
			m_Result.Add(spisok.GetTag(_T("сыр")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("томат")), 1 * count);
			counter++;
		}
		{
			//сэндвич
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("хлеб")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("масло")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("клубника")), 2 * count);
			counter++;
		}
		{
			//картошка фри
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("картофель")), 2 * count);
			m_Result.Add(spisok.GetTag(_T("сливки")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("томат")), 2 * count);
			counter++;
		}
		{
			//печеный картофель
			count = m_InputCount[counter];

			counter++;
		}
	}
};

class CRubberFactory : public CFactory
{
public:
	CRubberFactory()
	{
		m_TagInputArray.Add(spisok.GetTag(_T("резина")));
		m_TagInputArray.Add(spisok.GetTag(_T("пластик")));
		m_TagInputArray.Add(spisok.GetTag(_T("клей")));
		size_t count = m_TagInputArray.GetCount();
		m_Priority = PriorityTag::three;
		Init();
	}
	CString GetFactName()
	{
		return _T("Каучуковая фабрика");
	}
	void Calc()
	{
		
		int counter = 0;
		int count = 0;
		{
			//резина
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("каучук")), 1 * count);
			counter++;
		}
		{
			//пластик
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("каучук")), 3 * count);
			counter++;
		}
		{
			//клей
			count = m_InputCount[counter];
			
			counter++;
		}
	}
};

class CSugarFactory : public CFactory
{
public:
	CSugarFactory()
	{
		m_TagInputArray.Add(spisok.GetTag(_T("сахар")));
		m_TagInputArray.Add(spisok.GetTag(_T("сироп")));
		m_TagInputArray.Add(spisok.GetTag(_T("карамель")));
		m_TagInputArray.Add(spisok.GetTag(_T("медовая карамель")));
		m_Priority = PriorityTag::two;
		Init();
	}
	CString GetFactName()
	{
		return _T("Сахарная фабрика");
	}
	void Calc()
	{
		int counter = 0;
		int count = 0;
		{
			//сахар
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("сахарный тросник")), 1 * count);
			counter++;
		}
		{
			//сироп
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("сахарный тросник")), 2 * count);
			counter++;
		}
		{
			//карамель
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("сахарный тросник")), 3 * count);
			counter++;
		}
		{
			//медовая карамель
			count = m_InputCount[counter];

			counter++;
		}
	}
};

class CPaperFactory : public CFactory
{
public:
	CPaperFactory()
	{
		m_TagInputArray.Add(spisok.GetTag(_T("бумага")));
		m_TagInputArray.Add(spisok.GetTag(_T("бумажные полотенца")));
		m_TagInputArray.Add(spisok.GetTag(_T("обои")));
		m_TagInputArray.Add(spisok.GetTag(_T("книга")));
		m_Priority = PriorityTag::two;
		Init();
	}
	CString GetFactName()
	{
		return _T("Бумажная фабрика");
	}
	void Calc()
	{
		int counter = 0;
		int count = 0;
		{
			//бумага
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("сосна")), 1 * count);
			counter++;
		}
		{
			//бумажные полотенца
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("сосна")), 2 * count);
			counter++;
		}
		{
			//обои
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("сосна")), 2 * count);
			m_Result.Add(spisok.GetTag(_T("резина")), 1 * count);
			counter++;
		}
		{
			//книга
			count = m_InputCount[counter];

			counter++;
		}
	}
};

class CIceCreamFactory : public CFactory
{
public:
	CIceCreamFactory()
	{
		m_TagInputArray.Add(spisok.GetTag(_T("мороженное")));
		m_TagInputArray.Add(spisok.GetTag(_T("фруктовый лед")));
		m_TagInputArray.Add(spisok.GetTag(_T("замороженный йогурт")));
		m_TagInputArray.Add(spisok.GetTag(_T("эскимо")));
		m_TagInputArray.Add(spisok.GetTag(_T("ананасовый сорбет")));
		m_Priority = PriorityTag::three;
		Init();
	}
	CString GetFactName()
	{
		return _T("Фабрика мороженного");
	}
	void Calc()
	{
		int counter = 0;
		int count = 0;
		{
			//мороженное
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("молоко")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("сливки")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("сахар")), 1 * count);
			counter++;
		}
		{
			//фруктовый лед
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("клубника")), 2 * count);
			m_Result.Add(spisok.GetTag(_T("сахар")), 1 * count);
			counter++;
		}
		{
			//замороженный йогурт
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("йогурт")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("сливки")), 1 * count);
			counter++;
		}
		{
			//эскимо
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("сироп")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("какао")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("сосна")), 1 * count);
			counter++;
		}
		{
			//ананасовый сорбет
			count = m_InputCount[counter];

			counter++;
		}
	}
};

class СconfectioneryFactory : public CFactory
{
public:
	СconfectioneryFactory()
	{
		m_TagInputArray.Add(spisok.GetTag(_T("кекс")));
		m_TagInputArray.Add(spisok.GetTag(_T("шоколадный пирог")));
		m_TagInputArray.Add(spisok.GetTag(_T("пироженое")));
		m_TagInputArray .Add(spisok.GetTag(_T("пончик")));
		m_TagInputArray.Add(spisok.GetTag(_T("чизкейк")));
		m_Priority = PriorityTag::three;
		Init();
	}
	CString GetFactName()
	{
		return _T("Кондитерская");
	}
	void Calc()
	{
		int counter = 0;
		int count = 0;
		{
			//кекс
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("пшеница")), 3 * count);
			m_Result.Add(spisok.GetTag(_T("сахар")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("яйцо")), 4 * count);
			counter++;
		}
		{
			//шоколадный пирог
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("какао")), 2 * count);
			m_Result.Add(spisok.GetTag(_T("сироп")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("масло")), 1 * count);
			counter++;
		}
		{
			//пироженое
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("сахар")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("яйцо")), 5 * count);
			m_Result.Add(spisok.GetTag(_T("сливки")), 1 * count);
			counter++;
		}
		{
			//пончик
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("бублик")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("карамель")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("какао")), 1 * count);
			counter++;
		}
		{
			//чизкейк
			count = m_InputCount[counter];

			counter++;
		}
	}
};