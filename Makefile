PR=project
CON=$(PR)/Content
BIN=$(PR)/bin

build: server client
build-all: server client doc
rebuild: clean server client
rebuild-doc: clean-doc doc
clean-all: clean clean-doc
rebuild-all: clean clean-doc build doc

server:
	mdtool build project/project.csproj

client:
	cd $(CON); jade -P base.jade; \
	jade -Po partials partials/*.jade; \
	jade -Po partials/resource partials/resource/*.jade; \
	coffee --no-header -j scripts/app.js -bc scripts/*.coffee; \
	coffee --no-header -j scripts/controllers.js -bc scripts/controllers/*.coffee; \
	coffee --no-header -j scripts/services.js -bc scripts/services/*.coffee; cd -;

doc:
	cd $(PR); \
	doxygen doxygenrc; cd -;

clean:
	rm -rvf $(BIN)

clean-doc:
	rm -rvf $(PR)/doc 
