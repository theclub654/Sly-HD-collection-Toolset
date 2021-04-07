#include <iostream>
#include <fstream>

int main(int argc, const char** argv)
{
	const char* filename = argv[1];
	std::ifstream file(filename, std::ios::binary | std::ios::in | std::ios::beg);
	std::streampos size;
	static unsigned char* wal_buf;
	int fileVersion;
	int tocSize;

	if (file.is_open())
	{
		file.seekg(0, std::ios::end);
		size = file.tellg();
		file.seekg(0, std::ios::beg);
		wal_buf = new unsigned char[size];
		memset(wal_buf, 0, size);
		file.read((char*)wal_buf, size);
		std::memcpy(&fileVersion, wal_buf, 4);

		if (fileVersion == 64248363)
		{
			tocSize = 0x40000;
		}
		else if (fileVersion == 694)
		{
			tocSize = 0x40000;
		}
		else if (fileVersion == 64248469)
		{
			tocSize = 0x80000;
		}
		else if (fileVersion == 520)
		{
			tocSize = 0x80000;
		}
	}

	else
	{
		std::cout << "Error Opening File" << std::endl;
		return -1;
	}

	long long v0 = 0x7A69;
	for (int a = 0; a < tocSize; a++)
	{
		v0 *= 0x01A3;
		v0 += 0x181D;
		v0 %= 0x7262;
		auto a0 = v0;
		a0 = a0 << 8;
		a0 -= v0;
		a0 /= 0x7262;
		char a2 = wal_buf[a];
		a2 ^= a0;
		wal_buf[a] = a2;
	}


	std::ofstream output(filename, std::ios::binary | std::ios::out);
	output.write((char*)wal_buf, size);
	std::cout << "Successful" << std::endl;

	file.close();
	output.close();
	delete[]wal_buf;
}