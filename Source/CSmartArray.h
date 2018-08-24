#pragma once
#include "ProductTag.h"
#include <atlcoll.h>
#include <utility>
//CAllProductSpisok spisok;
class CSmartArray
{
	typedef CAtlMap<ProductTag, int>::CPair ProductCount;
	CAtlMap<ProductTag, int> m_TagMap;
public:
	void Add(ProductTag tag, int count)
	{
		if (count == 0)
			return;
		if (m_TagMap.Lookup(tag))
			m_TagMap[tag] += count;
		else
			m_TagMap[tag] = count;
	}
	void Add(const CString& name, int count)
	{
		ProductTag tag = spisok.GetTag(name);
		Add(tag, count);
	}
	void Merge(CSmartArray& Arr)
	{
		POSITION pos = Arr.m_TagMap.GetStartPosition();
		for (; pos != NULL;)
		{
			ProductCount* prCount = Arr.m_TagMap.GetNext(pos);
			Add(prCount->m_key, prCount->m_value);
		}
	}
	void ClearTag(ProductTag tag)
	{
		m_TagMap.RemoveKey(tag);
	}

	int GetTagCount(ProductTag tag)
	{
		int count = 0;
		if (m_TagMap.Lookup(tag))
			count = m_TagMap[tag];
		return count;
	}
	void RemoveAll()
	{
		m_TagMap.RemoveAll();
	}
	void Print()
	{
		POSITION pos = m_TagMap.GetStartPosition();
		for (; pos != NULL;)
		{
			ProductCount* prCount = m_TagMap.GetNext(pos);
			CString sCount;
			sCount.Format(_T(": %i \n\r"), prCount->m_value);
			OutputDebugString(spisok.GetName(prCount->m_key) + sCount);
		}
	}
	bool IsEmpty()
	{
		return m_TagMap.IsEmpty();
	}

	void ExcludeWhatHave(CSmartArray& Arr)
	{
		POSITION pos = m_TagMap.GetStartPosition();
		for (; pos != NULL;)
		{
			ProductCount* prCount = m_TagMap.GetNext(pos);
			ProductTag tag = prCount->m_key;
			if (int count = Arr.GetTagCount(tag))
			{
				if (prCount->m_value > count)
				{
					Add(tag, -count);
					Arr.ClearTag(tag);
				}
				else if (prCount->m_value < count)
				{
					ClearTag(tag);
					Arr.Add(tag, -prCount->m_value);
				}
				else //одинаковое значение
				{
					ClearTag(tag);
					Arr.ClearTag(tag);
				}
			}
		}
	}
};