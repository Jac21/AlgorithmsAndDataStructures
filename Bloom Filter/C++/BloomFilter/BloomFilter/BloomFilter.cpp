// BloomFilter.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

/*
	http://blog.michaelschmatz.com/2016/04/11/how-to-write-a-bloom-filter-cpp/
	Data structures which can efficiently determine whether an
	element is possible a member of a set or definitely not a 
	member of a set. Two parameters used to construct the Bloom
	filter: filter size (in bits), number of hash functions to use.
*/

// Structure contains a constructor, a function to add an 
// item to the bloom filter, and a function to query for an item
struct BloomFilter {
	BloomFilter(uint64_t size, uint8_t numHashes);

	void add(const uint8_t *data, std::size_t len);
	bool possiblyContains(const uint8_t *data, std::size_t len);

private:
	uint64_t m_size;	// filter size in bits
	uint8_t m_numHashes;	// number of hash functions to use
	std::vector<bool> m_bits;	// space efficient (one bit per element)
};

// constructor, records parameters and resizes bit array
BloomFilter::BloomFilter(uint64_t size, uint8_t numHashes) 
	: m_size(size), m_numHashes(numHashes) {
	m_bits.resize(size);
}

// 128 hash calculation function declaration
std::array<uint64_t, 2> hash(const uint8_t *data, std::size_t len) {
	std::array<uint64_t, 2> hashValue;
	MurmurHash3_x64_128(data, len, 0, hashValue.data());

	return hashValue;
}

// function to return output to the n^th hash function
inline uint64_t nthHash(uint8_t n,
	uint64_t hashA,
	uint64_t hashB,
	uint64_t filterSize) {
	return (hashA + n * hashB) % filterSize;
}

// set bits for item
void BloomFilter::add(const uint8_t *data, std::size_t len) {
	auto hashValues = hash(data, len);

	for (int n = 0; n < m_numHashes; n++) {
		m_bits[nthHash(n, hashValues[0], hashValues[1], m_size)] = true;
	}
}

// check bits for item
bool BloomFilter::possiblyContains(const uint8_t *data, std::size_t len) {
	auto hashValues = hash(data, len);

	for (int n = 0; n < m_numHashes; n++) {
		if (!m_bits[nthHash(n, hashValues[0], hashValues[1], m_size)]) {
			return false;
		}
	}

	return true;
}

int _tmain(int argc, _TCHAR* argv[])
{
	BloomFilter filter(((uint64_t)256), ((uint8_t)13));
	filter.add(((const uint8_t*)64), ((size_t)64));
	filter.possiblyContains(((const uint8_t*)64), ((size_t)64));

	return 0;
}

