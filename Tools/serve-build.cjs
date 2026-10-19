const http=require('node:http'),fs=require('node:fs'),path=require('node:path');
const root=path.resolve(process.argv[2]||'Builds'), port=Number(process.argv[3]||8091);
http.createServer((req,res)=>{
 let name;try{name=decodeURIComponent(new URL(req.url,'http://localhost').pathname)}catch{res.writeHead(400);return res.end()}
 let file=path.resolve(root,'.'+name);if(!file.startsWith(root+path.sep)&&file!==root){res.writeHead(403);return res.end()}
 if(fs.existsSync(file)&&fs.statSync(file).isDirectory())file=path.join(file,'index.html');
 if(!fs.existsSync(file)){console.warn('404 '+req.url);res.writeHead(404);return res.end('Not found')}
 const ext=path.extname(file),type={'.html':'text/html','.js':'application/javascript','.wasm':'application/wasm','.json':'application/json','.png':'image/png','.css':'text/css'}[ext]||'application/octet-stream';
 res.setHeader('Content-Type',type);res.setHeader('Cache-Control','no-store');res.setHeader('Content-Length',fs.statSync(file).size);
 if(ext==='.br'){res.setHeader('Content-Encoding','br');res.setHeader('Content-Type',file.endsWith('.wasm.br')?'application/wasm':file.endsWith('.js.br')?'application/javascript':'application/octet-stream')}
 if(ext==='.gz'){res.setHeader('Content-Encoding','gzip');res.setHeader('Content-Type',file.endsWith('.wasm.gz')?'application/wasm':file.endsWith('.js.gz')?'application/javascript':'application/octet-stream')}
 fs.createReadStream(file).pipe(res);
}).listen(port,'127.0.0.1',()=>console.log('Serving '+root+' at http://127.0.0.1:'+port));
