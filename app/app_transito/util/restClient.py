import requests
from requests.auth import HTTPBasicAuth
from requests.auth import to_native_string
import base64
class RestClient:
    def __init__(self, base_url, user, password):
        self.base_url = base_url
        self.user = user
        self.password = password

    def get(self, method, params):
        url = self.base_url
        if url!=None and len(url)>0 and method!=None and len(method)>0:
            url = url+'/'+method
        elif method!=None and len(method)>0:
            url = method
        with requests.Session() as sess:
            sess.headers['User-Agent'] = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36".encode('utf-8')
            sess.trust_env = False
         
            if self.user!=None and len(self.user) >0:    
                r = sess.get(url,params=params, auth=(self.user, self.password), timeout=240,verify=False)
            else:
                r= sess.get(url,params=params, timeout=240,verify=False)

            print (sess.headers)
            print (r.headers)
            return r

    def post(self, method, data):
        url = self.base_url
        
        if url!=None and len(url)>0 and method!=None and len(method)>0:
            url = url+'/'+method
        elif method!=None and len(method)>0:
            url = method
        headers = {'Content-type':'application/json; charset=utf-8','Accept':'text/plain'}
        if self.user!=None and len(self.user) >0: 
            r = requests.post(url,data=data,auth=(self.user, self.password),timeout=600,headers=headers,verify=False)
        else:
            r= requests.post(url,data=data,timeout=600,headers=headers,verify=False)
        return r

    def postSecure(self, method,clientid,clientsecret, data):
        url = self.base_url
        if url!=None and len(url)>0 and method!=None and len(method)>0:
            url = url+'/'+method
        elif method!=None and len(method)>0:
            url = method
        headers = {'Content-type':'application/json; charset=utf-8','Accept':'text/plain','client-id':clientid, 'client-secret':clientsecret}
        if self.user!=None and len(self.user) >0: 
            r = requests.post(url,data=data,auth=(self.user, self.password),timeout=600,headers=headers,verify=False)
        else:
            r= requests.post(url,data=data,timeout=600,headers=headers,verify=False)
        return r