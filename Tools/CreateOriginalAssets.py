"""Original procedural artwork and synth music for Crystal Escape. No downloaded art/audio."""
import sys, math, random, wave, struct
from pathlib import Path
from PIL import Image, ImageDraw, ImageFont
OUT = Path(sys.argv[1]) if len(sys.argv) > 1 else Path('Assets/CrystalEscape')
ART, AUDIO = OUT/'Art', OUT/'Audio'
ART.mkdir(parents=True, exist_ok=True)
AUDIO.mkdir(parents=True, exist_ok=True)
S=4
def sprite(name, paint):
    im=Image.new('RGBA',(128*S,128*S))
    d=ImageDraw.Draw(im)
    paint(d)
    im.resize((128,128),Image.Resampling.LANCZOS).save(ART/(name+'.png'))
def poly(d,pts,fill,outline=None,width=2):
    p=[(x*S,y*S) for x,y in pts]
    d.polygon(p,fill=fill)
    if outline:d.line(p+[p[0]],fill=outline,width=width*S,joint='curve')
def rect(d,b,fill,r=0,outline=None,w=2):
    d.rounded_rectangle(tuple(v*S for v in b),r*S,fill,outline,w*S)
def hero(d):
    rect(d,(19,18,109,113),'#061a2a',26)
    rect(d,(24,13,104,107),'#239ba8',22,'#88f5df',3)
    rect(d,(32,25,96,64),'#071927',12,'#56d7dc',2)
    rect(d,(39,34,89,48),'#b8fff3',6)
    rect(d,(34,76,94,94),'#103d53',6)
    rect(d,(56,74,72,97),'#f4bf73',3)
    for x in (14,101):rect(d,(x,53,x+13,85),'#244d65',5,'#80dcd9',2)
sprite('Explorer',hero)
def crystal(d):
    poly(d,[(64,5),(96,35),(103,80),(64,121),(25,80),(32,35)],'#153a67','#9bf9ff',3)
    poly(d,[(64,9),(64,78),(30,77),(35,36)],'#40cbe8')
    poly(d,[(64,9),(93,36),(99,77),(64,78)],'#6c7bec')
    poly(d,[(30,80),(64,80),(64,116)],'#b5ffed')
    poly(d,[(64,80),(99,80),(64,116)],'#437bc7')
    d.line([(39*S,37*S),(57*S,18*S)],fill='#e0ffff',width=4*S)
sprite('Crystal',crystal)
def drone(d,chase):
    col='#ff786b' if chase else '#ba9dff'
    dark='#762e45' if chase else '#514183'
    poly(d,[(64,12),(106,42),(109,88),(64,116),(19,88),(22,42)],'#0c172b',col,3)
    poly(d,[(64,24),(94,47),(93,81),(64,100),(35,81),(34,47)],dark,col,2)
    rect(d,(41,46,87,73),'#0a1023',10)
    if chase:poly(d,[(47,52),(64,60),(81,52),(77,67),(51,67)],'#ffd3a0')
    else:rect(d,(50,52,78,65),'#e5d9ff',6)
    for x in (8,106):rect(d,(x,51,x+14,85),dark,4,col,2)
sprite('PatrolDrone',lambda d:drone(d,False))
sprite('HunterDrone',lambda d:drone(d,True))
def machine(d):
    rect(d,(3,3,125,125),'#0a1425',10,'#2e637b',3)
    rect(d,(12,12,116,116),'#16334a',5,'#4487a1',2)
    for y in (29,45,61,77):rect(d,(26,y,102,y+5),'#091a29',2)
    rect(d,(24,94,74,102),'#63dfc6',2)
    rect(d,(83,94,103,102),'#f4c27a',2)
sprite('Reactor',machine)
floor=Image.new('RGB',(1024,640),'#081624');d=ImageDraw.Draw(floor)
for y in range(0,640,64):
    for x in range(0,1024,64):
        d.rounded_rectangle((x+2,y+2,x+61,y+61),5,fill=('#0c2132' if (x//64+y//64)%2==0 else '#0b1d2d'),outline='#153346')
        d.line((x+8,y+10,x+17,y+10),fill='#234555',width=1)
floor.save(ART/'LabFloor.png')

RATE=22050
def wav(name,samples):
    with wave.open(str(AUDIO/(name+'.wav')),'wb') as w:
        w.setnchannels(1);w.setsampwidth(2);w.setframerate(RATE)
        w.writeframes(b''.join(struct.pack('<h',int(max(-1,min(1,x))*32767)) for x in samples))
def tone(freq,t):return math.sin(2*math.pi*freq*t)+.22*math.sin(4*math.pi*freq*t)
def sfx(name,notes,step=.11):
    duration=len(notes)*step+.2
    samples=[]
    for i in range(int(RATE*duration)):
        t=i/RATE;j=min(int(t/step),len(notes)-1);local=t-j*step
        env=min(local/.008,1)*math.exp(-local*14)
        samples.append(.32*tone(notes[j],local)*env)
    wav(name,samples)
sfx('Collect',[659.25,987.77,1318.51],.075)
sfx('Victory',[329.63,392,493.88,659.25,783.99],.15)
sfx('Defeat',[329.63,293.66,246.94,164.81],.2)
random.seed(14)
wav('Hit',[(random.uniform(-1,1)*.2+math.sin(2*math.pi*(180-100*i/(RATE*.3))*i/RATE)*.3)*math.exp(-i/RATE*13) for i in range(int(RATE*.3))])
# Eight bars at 96 BPM, seamless 20 second loop. Em-C-G-D arpeggio, bass and pulse.
chords=[(164.81,196,246.94),(130.81,164.81,196),(196,246.94,293.66),(146.83,185,220)]
duration=20; samples=[]
for i in range(RATE*duration):
    t=i/RATE;bar=int(t/2.5)%4;chord=chords[bar];beat=t%.625;step=t%.3125
    arp=chord[int(t/.3125)%3]*2
    pad=sum(math.sin(2*math.pi*f*t) for f in chord)/3
    bass=math.sin(2*math.pi*chord[0]/2*t)*math.exp(-beat*6)
    pulse=math.sin(2*math.pi*65*t)*math.exp(-beat*22)
    melody=tone(arp,t)*math.exp(-step*12)
    fade=min(t/.03,(duration-t)/.03,1)
    samples.append((pad*.09+bass*.11+pulse*.05+melody*.05)*max(0,fade))
wav('ExtractionLoop',samples)
print('Generated 6 original textures and 5 original WAVs in',OUT)
