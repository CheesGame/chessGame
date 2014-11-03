//using C++
#include <iostream>
#include <cstdlib>
#include <cstring>
using namespace std;

int main(){
	char a[100];
	int b[100];
	int id=0;
	while(true){
		cin>>a;
		if(strcmp(a,"?")==0) break;
		else b[id++]=atoi(a);
		//cout<<b[id-1]<<endl;
	}
	return 0;
}