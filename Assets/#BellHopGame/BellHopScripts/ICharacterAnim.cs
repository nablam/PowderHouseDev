﻿public interface ICharacterAnim
{


    // to be used as anim event handler
    void AnimTossPeack();
    void AnimCatchPeack();


    //to be called by other objects
    void AnimateToss();
    void AnimateCatch();

    // void AnimWaveHello();


}
