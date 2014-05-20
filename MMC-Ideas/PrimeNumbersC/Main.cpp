//********************************************************************
// Main - <type description here>
// (c) Dez 2009 MMC
//********************************************************************

#include <iostream>

int main()
{
	unsigned int Sum   = 0;
	unsigned int Count = 0;

	const unsigned int Max = 500000000/2;
	//const unsigned int Max = 50/2;
	bool* Numbers = new bool[Max];
	for (unsigned int i=0;i<Max;i++) Numbers[i]=true;
	
	for (unsigned int idx = 1;idx<Max;idx++)
	{
		if (Numbers[idx])
		{
			unsigned int n = 2*idx+1;
			for(unsigned int i=idx+n;i<Max;i+=n)
				Numbers[i]=false;
			Count++;
			Sum+=n;
			//std::cout << n << std::endl;
		};
	};
	delete [] Numbers;

	std::cout << Count << ' ' << Sum << std::endl;
};

//********************************************************************
// END OF FILE Main
//********************************************************************
