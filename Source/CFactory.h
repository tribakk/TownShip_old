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
		m_TagInputArray.Add(spisok.GetTag(_T("������")));
		m_TagInputArray.Add(spisok.GetTag(_T("���")));
		m_TagInputArray.Add(spisok.GetTag(_T("�����")));
		m_TagInputArray.Add(spisok.GetTag(_T("������")));
		m_Priority = PriorityTag::one;
		Init();
	}

	CString GetFactName()
	{
		return _T("�������� �������");
	}
	void Calc()
	{
		int count = 0;
		int counter = 0;
		{
			//������
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("������")), 1 * count);
			counter++;
		}
		{
			//������
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("������")), 2 * count);
			counter++;
		}
		{
			//������
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("������")), 3 * count);
			counter++;
		}
		{
			//������
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("������")), 4 * count);
			counter++;
		}
	}

};

class CBakery : public CFactory
{
public:
	CBakery()
	{
		m_TagInputArray.Add(spisok.GetTag(_T("����")));
		m_TagInputArray.Add(spisok.GetTag(_T("�������")));
		m_TagInputArray.Add(spisok.GetTag(_T("������")));
		m_TagInputArray.Add(spisok.GetTag(_T("�����")));
		m_TagInputArray.Add(spisok.GetTag(_T("������������ ����")));
		m_TagInputArray.Add(spisok.GetTag(_T("��������� ����")));
		m_Priority = PriorityTag::one;
		Init();
	}
	CString GetFactName()
	{
		return _T("�������");
	}

	void Calc()
	{
		int count = 0;
		int counter = 0;
		{
			//����
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("�������")), 2 * count);
			counter++;
		}
		{
			//�������
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("�������")), 2 * count);
			m_Result.Add(spisok.GetTag(_T("����")), 2 * count);
			counter++;
		}
		{
			//������
			m_Result.Add(spisok.GetTag(_T("�������")), 2 * count);
			m_Result.Add(spisok.GetTag(_T("����")), 3 * count);
			m_Result.Add(spisok.GetTag(_T("�����")), 3 * count);
			counter++;
		}
		{
			//�����
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("�������")), 2 * count);
			m_Result.Add(spisok.GetTag(_T("���")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("�����")), 2 * count);
			counter++;
		}

		{
			//������������ ����
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("�������")), 2 * count);
			m_Result.Add(spisok.GetTag(_T("���������")), 2 * count);
			m_Result.Add(spisok.GetTag(_T("����")), 4 * count);
			counter++;
		}

		{
			//��������� ����
			count = m_InputCount[5];
		}
	}

};

class �WeavingFactory : public CFactory
{
public:
	�WeavingFactory()
	{
		m_TagInputArray.Add(spisok.GetTag(_T("��������� �����")));
		m_TagInputArray.Add(spisok.GetTag(_T("�����")));
		m_TagInputArray.Add(spisok.GetTag(_T("�������� �����")));
		m_Priority = PriorityTag::one;
		Init();
	}
	CString GetFactName()
	{
		return _T("������� �������");
	}
	void Calc()
	{
		int count = 0;
		int counter = 0;
		{
			//��������� �����
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("������")), 2 * count);
			counter++;
		}
		{
			//�����
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("������")), 2 * count);
			counter++;
		}
		{
			//�������� �����
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("����")), 2 * count);
			counter++;
		}
	}

};

class CSewingFactory : public CFactory
{
public:
	CSewingFactory()
	{
		m_TagInputArray.Add(spisok.GetTag(_T("�������")));
		m_TagInputArray.Add(spisok.GetTag(_T("������")));
		m_TagInputArray.Add(spisok.GetTag(_T("������")));
		m_TagInputArray.Add(spisok.GetTag(_T("�����")));
		m_TagInputArray.Add(spisok.GetTag(_T("������")));
		m_TagInputArray.Add(spisok.GetTag(_T("������")));
		m_Priority = PriorityTag::two;
		Init();
	}
	CString GetFactName()
	{
		return _T("������� �������");
	}

	void Calc()
	{
		int count = 0;
		int counter = 0;
		{
			//�������
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("��������� �����")), 1 * count);
			counter++;
		}
		{
			//������
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("�����")), 1 * count);
			counter++;
		}
		{
			//������
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("��������� �����")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("�����")), 1 * count);
			counter++;
		}
		{
			//�����
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("��������� �����")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("�������� �����")), 1 * count);
			counter++;
		}
		{
			//������
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("�����")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("�������� �����")), 1 * count);
			counter++;
		}
		{
			//������
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
		m_TagInputArray.Add(spisok.GetTag(_T("�������")));
		m_TagInputArray.Add(spisok.GetTag(_T("���������� �����")));
		m_TagInputArray.Add(spisok.GetTag(_T("�������")));
		m_TagInputArray.Add(spisok.GetTag(_T("�����")));
		m_TagInputArray.Add(spisok.GetTag(_T("������")));
		m_Priority = PriorityTag::three;
		Init();
	}
	CString GetFactName()
	{
		return _T("������� �������");
	}
	void Calc()
	{
		int count = 0;
		int counter = 0;
		{
			//�������
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("��������")), 2 * count);
			counter++;
		}
		{
			//���������� �����
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("��������")), 3 * count);
			counter++;
		}
		{
			//�������
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("�������")), 2 * count);
			m_Result.Add(spisok.GetTag(_T("��������")), 2 * count);
			counter++;
		}
		{
			//�����
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("���������")), 2 * count);
			counter++;
		}
		{
			//������
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
		m_TagInputArray.Add(spisok.GetTag(_T("��������")));
		m_TagInputArray.Add(spisok.GetTag(_T("���������")));
		m_TagInputArray.Add(spisok.GetTag(_T("�������")));
		m_TagInputArray.Add(spisok.GetTag(_T("�������� ���")));
		m_TagInputArray.Add(spisok.GetTag(_T("������� ���������")));
		m_Priority = PriorityTag::three;
		Init();
	}
	CString GetFactName()
	{
		return _T("������� ��������");
	}
	void Calc()
	{
		int count = 0;
		int counter = 0;
		{
			//��������
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("������")), 2 * count);
			m_Result.Add(spisok.GetTag(_T("��������")), 1 * count);
			counter++;
		}
		{
			//���������
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("����")), 2 * count);
			m_Result.Add(spisok.GetTag(_T("���")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("�����")), 1 * count);
			counter++;
		}
		{
			//�������
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("����")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("�����")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("��������")), 2 * count);
			counter++;
		}
		{
			//�������� ���
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("���������")), 2 * count);
			m_Result.Add(spisok.GetTag(_T("������")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("�����")), 2 * count);
			counter++;
		}
		{
			//������� ���������
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
		m_TagInputArray.Add(spisok.GetTag(_T("������")));
		m_TagInputArray.Add(spisok.GetTag(_T("�������")));
		m_TagInputArray.Add(spisok.GetTag(_T("����")));
		size_t count = m_TagInputArray.GetCount();
		m_Priority = PriorityTag::three;
		Init();
	}
	CString GetFactName()
	{
		return _T("���������� �������");
	}
	void Calc()
	{
		
		int counter = 0;
		int count = 0;
		{
			//������
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("������")), 1 * count);
			counter++;
		}
		{
			//�������
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("������")), 3 * count);
			counter++;
		}
		{
			//����
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
		m_TagInputArray.Add(spisok.GetTag(_T("�����")));
		m_TagInputArray.Add(spisok.GetTag(_T("�����")));
		m_TagInputArray.Add(spisok.GetTag(_T("��������")));
		m_TagInputArray.Add(spisok.GetTag(_T("������� ��������")));
		m_Priority = PriorityTag::two;
		Init();
	}
	CString GetFactName()
	{
		return _T("�������� �������");
	}
	void Calc()
	{
		int counter = 0;
		int count = 0;
		{
			//�����
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("�������� �������")), 1 * count);
			counter++;
		}
		{
			//�����
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("�������� �������")), 2 * count);
			counter++;
		}
		{
			//��������
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("�������� �������")), 3 * count);
			counter++;
		}
		{
			//������� ��������
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
		m_TagInputArray.Add(spisok.GetTag(_T("������")));
		m_TagInputArray.Add(spisok.GetTag(_T("�������� ���������")));
		m_TagInputArray.Add(spisok.GetTag(_T("����")));
		m_TagInputArray.Add(spisok.GetTag(_T("�����")));
		m_Priority = PriorityTag::two;
		Init();
	}
	CString GetFactName()
	{
		return _T("�������� �������");
	}
	void Calc()
	{
		int counter = 0;
		int count = 0;
		{
			//������
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("�����")), 1 * count);
			counter++;
		}
		{
			//�������� ���������
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("�����")), 2 * count);
			counter++;
		}
		{
			//����
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("�����")), 2 * count);
			m_Result.Add(spisok.GetTag(_T("������")), 1 * count);
			counter++;
		}
		{
			//�����
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
		m_TagInputArray.Add(spisok.GetTag(_T("����������")));
		m_TagInputArray.Add(spisok.GetTag(_T("��������� ���")));
		m_TagInputArray.Add(spisok.GetTag(_T("������������ ������")));
		m_TagInputArray.Add(spisok.GetTag(_T("������")));
		m_TagInputArray.Add(spisok.GetTag(_T("���������� ������")));
		m_Priority = PriorityTag::three;
		Init();
	}
	CString GetFactName()
	{
		return _T("������� �����������");
	}
	void Calc()
	{
		int counter = 0;
		int count = 0;
		{
			//����������
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("������")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("������")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("�����")), 1 * count);
			counter++;
		}
		{
			//��������� ���
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("��������")), 2 * count);
			m_Result.Add(spisok.GetTag(_T("�����")), 1 * count);
			counter++;
		}
		{
			//������������ ������
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("������")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("������")), 1 * count);
			counter++;
		}
		{
			//������
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("�����")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("�����")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("�����")), 1 * count);
			counter++;
		}
		{
			//���������� ������
			count = m_InputCount[counter];

			counter++;
		}
	}
};

class �confectioneryFactory : public CFactory
{
public:
	�confectioneryFactory()
	{
		m_TagInputArray.Add(spisok.GetTag(_T("����")));
		m_TagInputArray.Add(spisok.GetTag(_T("���������� �����")));
		m_TagInputArray.Add(spisok.GetTag(_T("���������")));
		m_TagInputArray .Add(spisok.GetTag(_T("������")));
		m_TagInputArray.Add(spisok.GetTag(_T("�������")));
		m_Priority = PriorityTag::three;
		Init();
	}
	CString GetFactName()
	{
		return _T("������������");
	}
	void Calc()
	{
		int counter = 0;
		int count = 0;
		{
			//����
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("�������")), 3 * count);
			m_Result.Add(spisok.GetTag(_T("�����")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("����")), 4 * count);
			counter++;
		}
		{
			//���������� �����
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("�����")), 2 * count);
			m_Result.Add(spisok.GetTag(_T("�����")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("�����")), 1 * count);
			counter++;
		}
		{
			//���������
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("�����")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("����")), 5 * count);
			m_Result.Add(spisok.GetTag(_T("������")), 1 * count);
			counter++;
		}
		{
			//������
			count = m_InputCount[counter];
			m_Result.Add(spisok.GetTag(_T("������")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("��������")), 1 * count);
			m_Result.Add(spisok.GetTag(_T("�����")), 1 * count);
			counter++;
		}
		{
			//�������
			count = m_InputCount[counter];

			counter++;
		}
	}
};