build: server client

rebuild: clean server client

rebuild-doc: clean-doc doc

server:
	mdtool build project/project.csproj

client:
	echo "build client not implented yet"

doc:
	doxygen project/doxygenrc


clean:
	rm -rvf project/bin/

clean-doc:
	rm doc -rvf


