from cx_Freeze import setup, Executable

base = None    

executables = [Executable("CapturarTransito.py", base=base)]

packages = ["idna"]
options = {
    'build_exe': {    
        'packages':packages,
    },    
}

setup(
    name = "CapturarTransito",
    options = options,
    version = "100",
    description = 'Capturar pax en transito',
    executables = executables
)