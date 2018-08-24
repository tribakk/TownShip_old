#pragma once
#include <atlcoll.h>


enum PriorityTag
{
	one = 1,
	two = 2,
	three = 3
};

enum ProductTag
{
	ptNotFound = 0,
	ptPhenica = 1,
	ptKukuruza = 2,
	ptMorkov = 3,
	ptSaharniTrosnik = 4,
	ptHlopok = 5,
	ptKlubnika = 6,
	ptTomat = 7,
	ptSosna = 8,
	ptKartofel = 9,
	ptKakao = 10,
	ptKauchuk = 11,
	ptShelk = 12,
	ptPerec = 13,
	ptMoloko = 14,
	ptYaco = 15,
	ptSherst = 16,
	ptHleb = 17,
	ptPechenie = 18,
	ptBublic = 19,
	ptPizza = 20,
	ptKartofelHleb = 21,
	ptBananaHleb = 22,
	ptClivki = 23,
	ptSyr = 24,
	ptMaslo = 25,
	ptIogury = 26,
	ptPersikIogurt = 27,
	ptHlopokTkan = 28,
	ptPryaja = 29,
	ptShelkNit = 30,
	ptRubashka = 31,
	ptSviter = 32,
	ptPalto = 33,
	ptShlapa = 34,
	ptPlatie = 35,
	ptKostum = 36,
	ptPopkorn = 37,
	ptKukuruzaChips = 38,
	ptGranola = 39,
	ptChips = 40,
	ptMilkSheik = 41,
	ptChisburger = 42,
	ptSendvich = 43,
	ptKartoshkaFree = 44,
	ptPecheneiKartofel = 45,
	ptRezina = 46,
	ptPlastik = 47,
	ptKley = 48,
	ptSahar = 49,
	ptSirop = 50,
	ptKaramel = 51,
	ptMedovayaKaramel = 52,
	ptBumaga = 53,
	ptBumagaPolotence = 54,
	ptOboi = 55,
	ptKniga = 56,
	ptMorogenoe = 57,
	ptFruktLed = 58,
	ptZamarojeniIogurt = 59,
	ptEscimo = 60,
	ptAnanasSorbet = 61,
	ptKeks = 62,
	ptShokoladPirog = 63,
	ptPirojenoe = 64,
	ptPoncik = 65,
	ptChizkeyk = 66,
	ptkanope = 67

};

class ProductTagString
{
public:
	ProductTag m_Tag;
	CString m_Name;
	ProductTagString(ProductTag tag, CString name)
		: m_Tag(tag)
		, m_Name(name)
	{

	}
};

class CAllProductSpisok
{
	CAtlArray<ProductTagString> m_Array;
public:
	CString GetName(ProductTag tag)
	{
		CString name;
		size_t count = m_Array.GetCount();
		for (size_t i = 0; i < count; i++)
		{
			if (m_Array[i].m_Tag == tag)
			{
				name = m_Array[i].m_Name;
				break;
			}
		}
		return name;
	}
	ProductTag GetTag(CString Name)
	{
		ProductTag tag = ProductTag::ptNotFound;
		size_t count = m_Array.GetCount();
		for (size_t i = 0; i < count; i++)
		{
			if (m_Array[i].m_Name.CompareNoCase(Name) == 0)
			{
				tag = m_Array[i].m_Tag;
				break;
			}
		}
		if (tag == ProductTag::ptNotFound)
		{
			OutputDebugString(_T("BIG ERROR: ") + Name + _T("\n\r\r"));
		}
			
		return tag;
	}

	CAllProductSpisok()
	{
		m_Array.Add(ProductTagString(ProductTag::ptPhenica, _T("�������")));
		m_Array.Add(ProductTagString(ProductTag::ptKukuruza, _T("��������")));
		m_Array.Add(ProductTagString(ProductTag::ptMorkov, _T("�������")));
		m_Array.Add(ProductTagString(ProductTag::ptSaharniTrosnik, _T("�������� �������")));
		m_Array.Add(ProductTagString(ProductTag::ptHlopok, _T("������")));
		m_Array.Add(ProductTagString(ProductTag::ptKlubnika, _T("��������")));
		m_Array.Add(ProductTagString(ProductTag::ptTomat, _T("�����")));
		m_Array.Add(ProductTagString(ProductTag::ptSosna, _T("�����")));
		m_Array.Add(ProductTagString(ProductTag::ptKartofel, _T("���������")));
		m_Array.Add(ProductTagString(ProductTag::ptKakao, _T("�����")));
		m_Array.Add(ProductTagString(ProductTag::ptKauchuk, _T("������")));
		m_Array.Add(ProductTagString(ProductTag::ptShelk, _T("����")));
		m_Array.Add(ProductTagString(ProductTag::ptPerec, _T("�����")));
		m_Array.Add(ProductTagString(ProductTag::ptMoloko, _T("������")));
		m_Array.Add(ProductTagString(ProductTag::ptYaco, _T("����")));
		m_Array.Add(ProductTagString(ProductTag::ptSherst, _T("������")));
		m_Array.Add(ProductTagString(ProductTag::ptHleb, _T("����")));
		m_Array.Add(ProductTagString(ProductTag::ptPechenie, _T("�������")));
		m_Array.Add(ProductTagString(ProductTag::ptBublic, _T("������")));
		m_Array.Add(ProductTagString(ProductTag::ptPizza, _T("�����")));
		m_Array.Add(ProductTagString(ProductTag::ptKartofelHleb, _T("������������ ����")));
		m_Array.Add(ProductTagString(ProductTag::ptBananaHleb, _T("��������� ����")));
		m_Array.Add(ProductTagString(ProductTag::ptClivki, _T("������")));
		m_Array.Add(ProductTagString(ProductTag::ptSyr, _T("���")));
		m_Array.Add(ProductTagString(ProductTag::ptMaslo, _T("�����")));
		m_Array.Add(ProductTagString(ProductTag::ptIogury, _T("������")));
		m_Array.Add(ProductTagString(ProductTag::ptPersikIogurt, _T("���������� ������")));
		m_Array.Add(ProductTagString(ProductTag::ptHlopokTkan, _T("��������� �����")));
		m_Array.Add(ProductTagString(ProductTag::ptPryaja, _T("�����")));
		m_Array.Add(ProductTagString(ProductTag::ptShelkNit, _T("�������� �����")));
		m_Array.Add(ProductTagString(ProductTag::ptRubashka, _T("�������")));
		m_Array.Add(ProductTagString(ProductTag::ptSviter, _T("������")));
		m_Array.Add(ProductTagString(ProductTag::ptPalto, _T("������")));
		m_Array.Add(ProductTagString(ProductTag::ptShlapa, _T("�����")));
		m_Array.Add(ProductTagString(ProductTag::ptPlatie, _T("������")));
		m_Array.Add(ProductTagString(ProductTag::ptKostum, _T("������")));
		m_Array.Add(ProductTagString(ProductTag::ptPopkorn, _T("�������")));
		m_Array.Add(ProductTagString(ProductTag::ptKukuruzaChips, _T("���������� �����")));
		m_Array.Add(ProductTagString(ProductTag::ptGranola, _T("�������")));
		m_Array.Add(ProductTagString(ProductTag::ptChips, _T("�����")));
		m_Array.Add(ProductTagString(ProductTag::ptkanope, _T("������")));
		m_Array.Add(ProductTagString(ProductTag::ptMilkSheik, _T("��������")));
		m_Array.Add(ProductTagString(ProductTag::ptChisburger, _T("���������")));
		m_Array.Add(ProductTagString(ProductTag::ptSendvich, _T("�������")));
		m_Array.Add(ProductTagString(ProductTag::ptKartoshkaFree, _T("�������� ���")));
		m_Array.Add(ProductTagString(ProductTag::ptPecheneiKartofel, _T("������� ���������")));
		m_Array.Add(ProductTagString(ProductTag::ptRezina, _T("������")));
		m_Array.Add(ProductTagString(ProductTag::ptPlastik, _T("�������")));
		m_Array.Add(ProductTagString(ProductTag::ptKley, _T("����")));
		m_Array.Add(ProductTagString(ProductTag::ptSahar, _T("�����")));
		m_Array.Add(ProductTagString(ProductTag::ptSirop, _T("�����")));
		m_Array.Add(ProductTagString(ProductTag::ptKaramel, _T("��������")));
		m_Array.Add(ProductTagString(ProductTag::ptMedovayaKaramel, _T("������� ��������")));
		m_Array.Add(ProductTagString(ProductTag::ptBumaga, _T("������")));
		m_Array.Add(ProductTagString(ProductTag::ptBumagaPolotence, _T("�������� ���������")));
		m_Array.Add(ProductTagString(ProductTag::ptOboi, _T("����")));
		m_Array.Add(ProductTagString(ProductTag::ptKniga, _T("�����")));
		m_Array.Add(ProductTagString(ProductTag::ptMorogenoe, _T("����������")));
		m_Array.Add(ProductTagString(ProductTag::ptFruktLed, _T("��������� ���")));
		m_Array.Add(ProductTagString(ProductTag::ptZamarojeniIogurt, _T("������������ ������")));
		m_Array.Add(ProductTagString(ProductTag::ptEscimo, _T("������")));
		m_Array.Add(ProductTagString(ProductTag::ptAnanasSorbet, _T("���������� ������")));
		m_Array.Add(ProductTagString(ProductTag::ptKeks, _T("����")));
		m_Array.Add(ProductTagString(ProductTag::ptShokoladPirog, _T("���������� �����")));
		m_Array.Add(ProductTagString(ProductTag::ptPirojenoe, _T("���������")));
		m_Array.Add(ProductTagString(ProductTag::ptPoncik, _T("������")));
		m_Array.Add(ProductTagString(ProductTag::ptChizkeyk, _T("�������")));
	}
};
CAllProductSpisok spisok;
