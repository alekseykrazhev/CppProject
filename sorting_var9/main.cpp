#include <iostream>
#include <vector>
#include <algorithm>
#include <fstream>
#include <string>
#include <sstream>

class Sort {
public:
    bool operator () (long long& a, long long& b) {
        long long count = 0, max_count_a = 0, max_count_b = 0;
        std::stringstream ss, ss1;
        ss<< a; ss1 << b;
        std::string a1 = ss.str(), b1 = ss1.str();

        for (int i = 0; i < a1.size() - 1; ++i) {
            if (a1[i] == a1[i + 1]) {
                ++count;
            }
            if (a1[i] != a1[i + 1] || i + 1 == a1.size() - 1) {
                if (count > max_count_a) {
                    max_count_a = count;
                    count = 0;
                }
            }
        }

        for (int i = 0; i < b1.size() - 1; ++i) {
            if (b1[i] == b1[i + 1]) {
                ++count;
            }
            if (b1[i] != b1[i + 1] || i + 1 == b1.size() - 1) {
                if (count > max_count_b) {
                    max_count_b = count;
                    count = 0;
                }
            }
        }

        return max_count_a > max_count_b;
    }
};

int main() {
    std::ifstream in ("/home/alex_turner/CLionProjects/sorting_var9/in.txt");
    std::ofstream out ("/home/alex_turner/CLionProjects/sorting_var9/out.txt");
    std::vector<long long> input;
    int a;
    while (in >> a) {
        input.push_back(a);
    }
    in.close();

    std::sort(input.begin(), input.end(), Sort());
    for (int i : input) {
        out << i << '\n';
    }
    out.close();
    return 0;
}
