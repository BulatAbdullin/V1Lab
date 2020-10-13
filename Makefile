CS=mcs
ASSEMBLIES=System.Numerics.dll

all: run

run: main.exe
	./main.exe


MYLIBS=data.dll,V1DataOnGrid.dll,V1DataCollection.dll,V1MainCollection.dll
main.exe: main.cs data.dll V1DataOnGrid.dll V1DataCollection.dll V1MainCollection.dll
	$(CS) -r:$(ASSEMBLIES) -r:$(MYLIBS) $<

V1MainCollection.dll: V1MainCollection.cs data.dll V1DataOnGrid.dll V1DataCollection.dll
	$(CS) -r:$(ASSEMBLIES) -r:data.dll,V1DataOnGrid,V1DataCollection.dll -t:library $<

V1DataOnGrid.dll: V1DataOnGrid.cs data.dll V1DataCollection.dll
	$(CS) -r:$(ASSEMBLIES) -r:data.dll,V1DataCollection.dll -t:library $<

V1DataCollection.dll: V1DataCollection.cs data.dll
	$(CS) -r:$(ASSEMBLIES) -r:data.dll -t:library $<

data.dll: data.cs
	$(CS) -r:$(ASSEMBLIES) -t:library $<

clean:
	rm *.{exe,dll}
