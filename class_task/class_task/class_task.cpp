#include <iostream>
#include <fstream>
#include <vector>

int main() {
    std::ifstream in("C:\\Users\\Alex\\Desktop\\Project1\\class_task\\in.txt");
    if (!in.is_open()) {
        std::cerr << "ERROR OPENING FILE";
        return 1;
    }
    int a, find;
    const int amount = 5;
    bool is_found = false;
    std::vector<int> vec;
    while (in >> a) {
        vec.push_back(a);
    }
    for (int i = 0; i < amount; ++i) {
        std::cout << "Enter a number : ";
        std::cin >> find;
        for (int j = 0; j < vec.size(); ++j) {
            if (vec[j] == find) {
                std::cout << "Number " << find << " is in array:)\n";
                is_found = true;
                continue;
            }
        }
        if (!is_found) {
            std::cout << "Number " << find << " isn't in array:(\n";
        }
        is_found = false;
    }
    return 0;
}