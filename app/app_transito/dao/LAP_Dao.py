import mysql.connector
import json

class Lap_Dao:
    def __init__(self,_host, _database, _user, _password):
        self.host = _host
        self.database = _database
        self.user = _user
        self.password = _password

    def EjecutarQueryDict(self,query):
        if query == None:
            return None
        try:
            cnx = mysql.connector.connect(user=self.user, password=self.password,
                                host=self.host,
                                database=self.database)
            cursor = cnx.cursor()
            cursor.execute(query)
            columns = [d[0] for d in cursor.description]
            return [dict(zip(columns,row)) for row in cursor.fetchall()]

        except Exception as ex:
            raise ex
        finally:
            cursor.close()
            cnx.close()
    
    def EjecutarQueryJson(self,query):
        return json.dumps(self.EjecutarQueryDict(query))

    def Ejecutar(self,query):
        if query == None:
            return None
        try:
            cnx = mysql.connector.connect(user=self.user, password=self.password,
                                host=self.host,
                                database=self.database)
            cursor = cnx.cursor()
            cursor.execute(query)
        except Exception as ex:
            raise ex
        finally:
            cursor.close()
            cnx.close()
        