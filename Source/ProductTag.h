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
		m_Array.Add(ProductTagString(ProductTag::ptPhenica, _T("пшеница")));
		m_Array.Add(ProductTagString(ProductTag::ptKukuruza, _T("кукуруза")));
		m_Array.Add(ProductTagString(ProductTag::ptMorkov, _T("морковь")));
		m_Array.Add(ProductTagString(ProductTag::ptSaharniTrosnik, _T("сахарный тросник")));
		m_Array.Add(ProductTagString(ProductTag::ptHlopok, _T("хлопок")));
		m_Array.Add(ProductTagString(ProductTag::ptKlubnika, _T("клубника")));
		m_Array.Add(ProductTagString(ProductTag::ptTomat, _T("томат")));
		m_Array.Add(ProductTagString(ProductTag::ptSosna, _T("сосна")));
		m_Array.Add(ProductTagString(ProductTag::ptKartofel, _T("картофель")));
		m_Array.Add(ProductTagString(ProductTag::ptKakao, _T("какао")));
		m_Array.Add(ProductTagString(ProductTag::ptKauchuk, _T("каучук")));
		m_Array.Add(ProductTagString(ProductTag::ptShelk, _T("шелк")));
		m_Array.Add(ProductTagString(ProductTag::ptPerec, _T("перец")));
		m_Array.Add(ProductTagString(ProductTag::ptMoloko, _T("молоко")));
		m_Array.Add(ProductTagString(ProductTag::ptYaco, _T("€йцо")));
		m_Array.Add(ProductTagString(ProductTag::ptSherst, _T("шерсть")));
		m_Array.Add(ProductTagString(ProductTag::ptHleb, _T("хлеб")));
		m_Array.Add(ProductTagString(ProductTag::ptPechenie, _T("печенье")));
		m_Array.Add(ProductTagString(ProductTag::ptBublic, _T("бублик")));
		m_Array.Add(ProductTagString(ProductTag::ptPizza, _T("пицца")));
		m_Array.Add(ProductTagString(ProductTag::ptKartofelHleb, _T("картофельный хлеб")));
		m_Array.Add(ProductTagString(ProductTag::ptBananaHleb, _T("банановый хлеб")));
		m_Array.Add(ProductTagString(ProductTag::ptClivki, _T("сливки")));
		m_Array.Add(ProductTagString(ProductTag::ptSyr, _T("сыр")));
		m_Array.Add(ProductTagString(ProductTag::ptMaslo, _T("масло")));
		m_Array.Add(ProductTagString(ProductTag::ptIogury, _T("йогурт")));
		m_Array.Add(ProductTagString(ProductTag::ptPersikIogurt, _T("персиковый йогурт")));
		m_Array.Add(ProductTagString(ProductTag::ptHlopokTkan, _T("хлопкова€ ткань")));
		m_Array.Add(ProductTagString(ProductTag::ptPryaja, _T("пр€жа")));
		m_Array.Add(ProductTagString(ProductTag::ptShelkNit, _T("шелкова€ ткань")));
		m_Array.Add(ProductTagString(ProductTag::ptRubashka, _T("рубашка")));
		m_Array.Add(ProductTagString(ProductTag::ptSviter, _T("свитер")));
		m_Array.Add(ProductTagString(ProductTag::ptPalto, _T("пальто")));
		m_Array.Add(ProductTagString(ProductTag::ptShlapa, _T("шл€па")));
		m_Array.Add(ProductTagString(ProductTag::ptPlatie, _T("платье")));
		m_Array.Add(ProductTagString(ProductTag::ptKostum, _T("костюм")));
		m_Array.Add(ProductTagString(ProductTag::ptPopkorn, _T("попкорн")));
		m_Array.Add(ProductTagString(ProductTag::ptKukuruzaChips, _T("кукурузные чипсы")));
		m_Array.Add(ProductTagString(ProductTag::ptGranola, _T("гранола")));
		m_Array.Add(ProductTagString(ProductTag::ptChips, _T("чипсы")));
		m_Array.Add(ProductTagString(ProductTag::ptkanope, _T("канопе")));
		m_Array.Add(ProductTagString(ProductTag::ptMilkSheik, _T("милкшейк")));
		m_Array.Add(ProductTagString(ProductTag::ptChisburger, _T("чизбургер")));
		m_Array.Add(ProductTagString(ProductTag::ptSendvich, _T("сэндвич")));
		m_Array.Add(ProductTagString(ProductTag::ptKartoshkaFree, _T("картошка фри")));
		m_Array.Add(ProductTagString(ProductTag::ptPecheneiKartofel, _T("печеный картофель")));
		m_Array.Add(ProductTagString(ProductTag::ptRezina, _T("резина")));
		m_Array.Add(ProductTagString(ProductTag::ptPlastik, _T("пластик")));
		m_Array.Add(ProductTagString(ProductTag::ptKley, _T("клей")));
		m_Array.Add(ProductTagString(ProductTag::ptSahar, _T("сахар")));
		m_Array.Add(ProductTagString(ProductTag::ptSirop, _T("сироп")));
		m_Array.Add(ProductTagString(ProductTag::ptKaramel, _T("карамель")));
		m_Array.Add(ProductTagString(ProductTag::ptMedovayaKaramel, _T("медова€ карамель")));
		m_Array.Add(ProductTagString(ProductTag::ptBumaga, _T("бумага")));
		m_Array.Add(ProductTagString(ProductTag::ptBumagaPolotence, _T("бумажные полотенца")));
		m_Array.Add(ProductTagString(ProductTag::ptOboi, _T("обои")));
		m_Array.Add(ProductTagString(ProductTag::ptKniga, _T("книга")));
		m_Array.Add(ProductTagString(ProductTag::ptMorogenoe, _T("мороженное")));
		m_Array.Add(ProductTagString(ProductTag::ptFruktLed, _T("фруктовый лед")));
		m_Array.Add(ProductTagString(ProductTag::ptZamarojeniIogurt, _T("замороженный йогурт")));
		m_Array.Add(ProductTagString(ProductTag::ptEscimo, _T("эскимо")));
		m_Array.Add(ProductTagString(ProductTag::ptAnanasSorbet, _T("ананасовый сорбет")));
		m_Array.Add(ProductTagString(ProductTag::ptKeks, _T("кекс")));
		m_Array.Add(ProductTagString(ProductTag::ptShokoladPirog, _T("шоколадный пирог")));
		m_Array.Add(ProductTagString(ProductTag::ptPirojenoe, _T("пироженое")));
		m_Array.Add(ProductTagString(ProductTag::ptPoncik, _T("пончик")));
		m_Array.Add(ProductTagString(ProductTag::ptChizkeyk, _T("чизкейк")));
	}
};
CAllProductSpisok spisok;
