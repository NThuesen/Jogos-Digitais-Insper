// Successful automation actions are not assertions about the game outcome.
const {chromium}=require('../Temp/browser/node_modules/playwright-core');
const fs=require('node:fs'),readline=require('node:readline'),path=require('node:path');
(async()=>{
 const folder=path.resolve(process.env.CRYSTAL_QA_OUTPUT||'docs/evidencias-webgl');fs.mkdirSync(folder,{recursive:true});
 const logs=[],errors=[];
 const browser=await chromium.launch({executablePath:'C:/Program Files (x86)/Microsoft/Edge/Application/msedge.exe',headless:true,args:['--use-angle=swiftshader','--enable-unsafe-swiftshader']});
 const page=await browser.newPage({viewport:{width:1100,height:780}});
 page.on('console',m=>{logs.push({type:m.type(),text:m.text(),location:m.location()});if(m.type()==='error')errors.push({text:m.text(),location:m.location()})});
 page.on('pageerror',e=>errors.push(e.message));
 page.on('response',r=>{if(r.status()>=400)errors.push({status:r.status(),url:r.url()})});
 page.on('requestfailed',r=>errors.push({url:r.url(),failure:r.failure()}));
 await page.goto(process.argv[2]||'http://127.0.0.1:8091/CrystalEscape-WebGL/',{waitUntil:'domcontentloaded',timeout:30000});
 await page.waitForFunction(()=>document.getElementById('unity-loading-bar')?.style.display==='none',{},{timeout:180000});
 await page.waitForTimeout(4500);
 await page.screenshot({path:path.join(folder,'00-loaded.png')});
 console.log(JSON.stringify({ready:true,title:await page.title(),canvas:await page.locator('canvas').boundingBox(),errors}));
 const shot=async name=>{const filename=path.join(folder,name+'.png');await page.screenshot({path:filename});return filename};
 const status=async()=>({url:page.url(),title:await page.title(),errors,console:logs.slice(-15)});
 fs.writeFileSync(path.join(folder,'browser-console.json'),JSON.stringify({logs,errors},null,2));
 const rl=process.argv[3]?JSON.parse(fs.readFileSync(process.argv[3],'utf8')):readline.createInterface({input:process.stdin});
 for await(const line of rl){
  try{
   const a=typeof line==='string'?JSON.parse(line):line;
   if(a.type==='exit'){console.log(JSON.stringify(await status()));break}
   if(a.type==='click')await page.locator('canvas').click({position:{x:a.x,y:a.y}});
   if(a.type==='press'){await page.keyboard.down(a.key);await page.waitForTimeout(a.ms||100);await page.keyboard.up(a.key)}
   if(a.type==='wait')await page.waitForTimeout(a.ms);
   if(a.type==='reload'){await page.reload();await page.waitForFunction(()=>document.getElementById('unity-loading-bar')?.style.display==='none',{},{timeout:180000});await page.waitForTimeout(1000)}
   if(a.type==='route'){
    await page.locator('canvas').focus();await page.keyboard.down('Enter');await page.waitForTimeout(120);await page.keyboard.up('Enter');await page.waitForTimeout(900);
    const steps=[['ArrowDown',1200],['ArrowUp',1220],['ArrowRight',1050],['ArrowRight',1450],['ArrowDown',1200],['ArrowLeft',430],['ArrowLeft',200]];
    for(let i=0;i<steps.length;i++){await page.keyboard.down(steps[i][0]);await page.waitForTimeout(steps[i][1]);await page.keyboard.up(steps[i][0]);await page.waitForTimeout(100);await shot('route-'+i)}
   }
   if(a.type==='resize')await page.setViewportSize({width:a.width,height:a.height});
   await page.waitForTimeout(100);console.log(JSON.stringify({ok:true,action:a.type,screenshot:a.name?await shot(a.name):null,...await status()}));
   fs.writeFileSync(path.join(folder,'browser-console.json'),JSON.stringify({logs,errors},null,2));
  }catch(e){console.log(JSON.stringify({ok:false,error:e.message}))}
 }
 await browser.close();
})().catch(e=>{console.error(e);process.exit(1)});
