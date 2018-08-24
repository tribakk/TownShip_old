// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

// TownShip.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

class AA
{

};

template <class A> class A1
{

};

class BB
{
	A1<AA> a1;
};

int main()
{
	BB* b;
	int* a = (int*)b;
	int* a1 = static_cast<int*>(b);
	return 0;
}
