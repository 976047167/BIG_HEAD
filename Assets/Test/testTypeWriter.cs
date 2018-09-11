using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testTypeWriter : MonoBehaviour
{
    TypewriterEffect typewriter;
    UILabel label;
    UIScrollView scrollView;
    float lastHeight;
    // Use this for initialization
    void Start()
    {
        typewriter = GetComponent<TypewriterEffect>();
        label = GetComponent<UILabel>();
        scrollView = transform.parent.GetComponent<UIScrollView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            label.text = "目的　[n]探索激光显微技术(laser capture microdissection system, LCM)捕获口腔上皮细胞，" +
                "并进行STR-DNA分型检测的方法。方法　用VERITAS显微切割仪红外低能激光显微捕获一定数量口腔上皮细胞，" +
                "进行Profiler Plus试剂盒STR复合扩增，检测DNA基因型。" +
                "结果　7~8个口腔上皮细胞能成功获得STR-DNA分型。3~4个口腔上皮细胞不能成功获得STR-DNA分型。" +
                "结论　激光显微捕获作为一种分离单个细胞的新技术，对于微量口腔上皮细胞的STR-DNA分型是可行的。特异性高\n" +
@"首次报导的PCR所用的DNA聚合酶是大肠杆菌的DNAPolymerase I的Klenow大片段，其酶活性在90℃会变性失活，需每次PCR循环都要重新加入Klenow大片段，同时引物是在37℃延伸（聚合）易产生模板—引物之间的碱基错配、致特异性较差，1988年Saiki等从温泉水中分离到的水生嗜热杆菌中提取的热稳定的Taq DNA聚合酶，在热变性处理时不被灭活，不必在每次循环扩增中再加入新酶，可以在较高温度下连续反应，显著地提高PCR产物的特异性，序列分析证明其扩增的DNA序列与原模板DNA一致。
扩增过程中，单核苷酸的错误参入程度很低、其错配率一般只有约万分之一，足可以提供特异性分析，选用各型病毒相对的特异寡核苷酸引物。PCR能一次确定病毒的多重感染。如用HPV11和HPV16型病毒引物检测病妇宫颈刮片细胞可以发现部分病人存在HPV11和HPV16两型的双重感染。
高度敏感
理论上PCR可以按2n倍数扩增DNA十亿倍以上，实际应用已证实可以将极微量的靶DNA成百万倍以上地扩增到足够检测分析量的DNA。能从100万个细胞中检出一个靶细胞，或对诸如病人口液等只含一个感染细胞的标本或仅含0.01pg的感染细胞的特异性片段样品中均可检测。
无放射性
一般在2小时内约可完成30次以上的循环扩增，加上用电泳分析。只需3 - 4小时便可完成，不用分离提纯病毒，DNA粗制品及总RNA均可作为反应起始物，可直接用临床标本如血液、体液、尿液、洗液、脱落毛发、细胞、活体组织等粗制的DNA的提取液来扩增检测，省去费时繁杂的提纯程序，扩增产物用一般电泳分析即可，不一定用同位素，无放射性易于推广。
简便
扩增产物可直接供作序列分析和分子克隆，摆脱繁琐的基因方法，可直接从RNA或染色体DNA中或部分DNA已降解的样品中分离目的基因，省去常规方法中须先进行克隆后再作序列分析的冗繁程序。已固定的和包埋的组织或切片亦可检测。如在PCR引物端事先构建一个内切酶位点，扩增的靶DNA可直接克隆到M13，PUC19等相应酶切位点的载体中。
可扩增物质
先按通常方法用寡脱氧胸苷引物和逆转录酶将mRNA转变成单链cDNA，再将得到的单链cDNA进行PCR扩增，即使mRNA转录片段只有100ngcDNA中的0.01 %，也能经PCR扩增1ng有242碱基对长度的特异片段，有些外显子分散在一段很长的DNA中，难以将整段DNA大分子扩增和做序列分析。若以mRNA作模板，则可将外显子集中，用PCR一次便完成对外显子的扩增并进行序列分析。";
            typewriter.ResetToBeginning();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            //scrollView.UpdatePosition();
            //scrollView.OnPan(new Vector2(0f, 1000f));

        }
        if (label.height != lastHeight)
        {

            scrollView.ResetPosition();
            scrollView.SetDragAmount(0f, 1f, false);
            lastHeight = label.height;
        }
    }
}
