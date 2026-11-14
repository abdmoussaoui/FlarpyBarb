from pathlib import Path
import math, wave, struct
out=Path('Assets/Resources/Audio')
out.mkdir(parents=True, exist_ok=True)
rate=22050

def save(name, samples):
    assert samples and max(abs(x) for x in samples)<1
    with wave.open(str(out/(name+'.wav')),'wb') as f:
        f.setparams((1,2,rate,len(samples),'NONE','not compressed'))
        f.writeframes(struct.pack('<'+'h'*len(samples),*(int(x*32767) for x in samples)))

def effect(name,duration,freq,volume):
    samples=[]
    phase=0
    for i in range(int(rate*duration)):
        t=i/rate
        phase+=2*math.pi*freq(t/duration)/rate
        env=min(t/.008,1)*(1-t/duration)**2
        samples.append(volume*env*(math.sin(phase)+.18*math.sin(2*phase))/1.18)
    save(name,samples)
effect('flap',.13,lambda t:380+650*t,.32)
effect('score',.32,lambda t:880 if t<.45 else 1320,.38)
effect('crash',.5,lambda t:240*(1-t)+45,.48)
# Eight-bar original arpeggio; each note releases before the next loop boundary.
notes=[60,67,72,76,57,64,69,72,53,60,65,69,55,62,67,71]
step=.3
length=step*64
samples=[0.0]*round(rate*length)
for beat in range(64):
    midi=notes[(beat//4)*1%16] if beat%4==0 else notes[(beat//4+beat%4)%16]+12
    hz=440*2**((midi-69)/12)
    for j in range(int(step*rate)):
        t=j/rate
        env=min(t/.018,1)*max(0,1-t/(step*.92))**2
        index=round(beat*step*rate)+j
        if index<len(samples): samples[index]+=.15*env*(math.sin(2*math.pi*hz*t)+.2*math.sin(4*math.pi*hz*t))/1.2
save('music',samples)
for p in out.glob('*.wav'):
    with wave.open(str(p)) as f:
        assert f.getnchannels()==1 and f.getsampwidth()==2
        print(p,round(f.getnframes()/f.getframerate(),2),'seconds')
