using System.Xml.Serialization;
using System.Text;

namespace Utility;

public class HotpepperAdapter
{
    private static readonly string HotpepperUrl = "http://webservice.recruit.co.jp/hotpepper/gourmet/v1/?";
    private string HotpepperKey { get; }
    private HttpClient HttpClient { get; }

    public HotpepperAdapter(string key, HttpClient httpClient)
    {
        HotpepperKey = key;
        HttpClient = httpClient;
    }

    public async Task<List<Shop>> GetResultAsync(double lat, double lng)
    {
        if (lat == 0 && lng == 0) return new List<Shop>();
        var str = await HttpClient.GetStringAsync($"{HotpepperUrl}key={HotpepperKey}&lat={lat}&lng={lng}");
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(str));
        var xml = new XmlSerializer(typeof(results)).Deserialize(stream) as results;
        return xml.shop.Select(x => new Shop(x.name, x.address, (double)x.lat, (double)x.lng)).ToList();
    }
}

public record Shop(string Name, string Address, double Lat, double Lng);

// メモ: 生成されたコードは、少なくとも .NET Framework 4.5または .NET Core/Standard 2.0 が必要な可能性があります。
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://webservice.recruit.co.jp/HotPepper/")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://webservice.recruit.co.jp/HotPepper/", IsNullable = false)]
public partial class results
{

    private decimal api_versionField;

    private byte results_availableField;

    private byte results_returnedField;

    private byte results_startField;

    private resultsShop[] shopField;

    /// <remarks/>
    public decimal api_version
    {
        get
        {
            return this.api_versionField;
        }
        set
        {
            this.api_versionField = value;
        }
    }

    /// <remarks/>
    public byte results_available
    {
        get
        {
            return this.results_availableField;
        }
        set
        {
            this.results_availableField = value;
        }
    }

    /// <remarks/>
    public byte results_returned
    {
        get
        {
            return this.results_returnedField;
        }
        set
        {
            this.results_returnedField = value;
        }
    }

    /// <remarks/>
    public byte results_start
    {
        get
        {
            return this.results_startField;
        }
        set
        {
            this.results_startField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("shop")]
    public resultsShop[] shop
    {
        get
        {
            return this.shopField;
        }
        set
        {
            this.shopField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://webservice.recruit.co.jp/HotPepper/")]
public partial class resultsShop
{

    private string idField;

    private string nameField;

    private string logo_imageField;

    private string name_kanaField;

    private string addressField;

    private string station_nameField;

    private byte ktai_couponField;

    private resultsShopLarge_service_area large_service_areaField;

    private resultsShopService_area service_areaField;

    private resultsShopLarge_area large_areaField;

    private resultsShopMiddle_area middle_areaField;

    private resultsShopSmall_area small_areaField;

    private decimal latField;

    private decimal lngField;

    private resultsShopGenre genreField;

    private resultsShopBudget budgetField;

    private string budget_memoField;

    private string catchField;

    private byte capacityField;

    private string accessField;

    private string mobile_accessField;

    private resultsShopUrls urlsField;

    private string openField;

    private string closeField;

    private string party_capacityField;

    private string wifiField;

    private string other_memoField;

    private string shop_detail_memoField;

    private string weddingField;

    private string free_drinkField;

    private string free_foodField;

    private string private_roomField;

    private string horigotatsuField;

    private string tatamiField;

    private string cardField;

    private string non_smokingField;

    private string charterField;

    private string parkingField;

    private string barrier_freeField;

    private string showField;

    private string karaokeField;

    private string bandField;

    private string tvField;

    private string englishField;

    private string petField;

    private string childField;

    private resultsShopCoupon_urls coupon_urlsField;

    private string courseField;

    private resultsShopPhoto photoField;

    private string lunchField;

    private string midnightField;

    /// <remarks/>
    public string id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    public string logo_image
    {
        get
        {
            return this.logo_imageField;
        }
        set
        {
            this.logo_imageField = value;
        }
    }

    /// <remarks/>
    public string name_kana
    {
        get
        {
            return this.name_kanaField;
        }
        set
        {
            this.name_kanaField = value;
        }
    }

    /// <remarks/>
    public string address
    {
        get
        {
            return this.addressField;
        }
        set
        {
            this.addressField = value;
        }
    }

    /// <remarks/>
    public string station_name
    {
        get
        {
            return this.station_nameField;
        }
        set
        {
            this.station_nameField = value;
        }
    }

    /// <remarks/>
    public byte ktai_coupon
    {
        get
        {
            return this.ktai_couponField;
        }
        set
        {
            this.ktai_couponField = value;
        }
    }

    /// <remarks/>
    public resultsShopLarge_service_area large_service_area
    {
        get
        {
            return this.large_service_areaField;
        }
        set
        {
            this.large_service_areaField = value;
        }
    }

    /// <remarks/>
    public resultsShopService_area service_area
    {
        get
        {
            return this.service_areaField;
        }
        set
        {
            this.service_areaField = value;
        }
    }

    /// <remarks/>
    public resultsShopLarge_area large_area
    {
        get
        {
            return this.large_areaField;
        }
        set
        {
            this.large_areaField = value;
        }
    }

    /// <remarks/>
    public resultsShopMiddle_area middle_area
    {
        get
        {
            return this.middle_areaField;
        }
        set
        {
            this.middle_areaField = value;
        }
    }

    /// <remarks/>
    public resultsShopSmall_area small_area
    {
        get
        {
            return this.small_areaField;
        }
        set
        {
            this.small_areaField = value;
        }
    }

    /// <remarks/>
    public decimal lat
    {
        get
        {
            return this.latField;
        }
        set
        {
            this.latField = value;
        }
    }

    /// <remarks/>
    public decimal lng
    {
        get
        {
            return this.lngField;
        }
        set
        {
            this.lngField = value;
        }
    }

    /// <remarks/>
    public resultsShopGenre genre
    {
        get
        {
            return this.genreField;
        }
        set
        {
            this.genreField = value;
        }
    }

    /// <remarks/>
    public resultsShopBudget budget
    {
        get
        {
            return this.budgetField;
        }
        set
        {
            this.budgetField = value;
        }
    }

    /// <remarks/>
    public string budget_memo
    {
        get
        {
            return this.budget_memoField;
        }
        set
        {
            this.budget_memoField = value;
        }
    }

    /// <remarks/>
    public string @catch
    {
        get
        {
            return this.catchField;
        }
        set
        {
            this.catchField = value;
        }
    }

    /// <remarks/>
    public byte capacity
    {
        get
        {
            return this.capacityField;
        }
        set
        {
            this.capacityField = value;
        }
    }

    /// <remarks/>
    public string access
    {
        get
        {
            return this.accessField;
        }
        set
        {
            this.accessField = value;
        }
    }

    /// <remarks/>
    public string mobile_access
    {
        get
        {
            return this.mobile_accessField;
        }
        set
        {
            this.mobile_accessField = value;
        }
    }

    /// <remarks/>
    public resultsShopUrls urls
    {
        get
        {
            return this.urlsField;
        }
        set
        {
            this.urlsField = value;
        }
    }

    /// <remarks/>
    public string open
    {
        get
        {
            return this.openField;
        }
        set
        {
            this.openField = value;
        }
    }

    /// <remarks/>
    public string close
    {
        get
        {
            return this.closeField;
        }
        set
        {
            this.closeField = value;
        }
    }

    /// <remarks/>
    public string party_capacity
    {
        get
        {
            return this.party_capacityField;
        }
        set
        {
            this.party_capacityField = value;
        }
    }

    /// <remarks/>
    public string wifi
    {
        get
        {
            return this.wifiField;
        }
        set
        {
            this.wifiField = value;
        }
    }

    /// <remarks/>
    public string other_memo
    {
        get
        {
            return this.other_memoField;
        }
        set
        {
            this.other_memoField = value;
        }
    }

    /// <remarks/>
    public string shop_detail_memo
    {
        get
        {
            return this.shop_detail_memoField;
        }
        set
        {
            this.shop_detail_memoField = value;
        }
    }

    /// <remarks/>
    public string wedding
    {
        get
        {
            return this.weddingField;
        }
        set
        {
            this.weddingField = value;
        }
    }

    /// <remarks/>
    public string free_drink
    {
        get
        {
            return this.free_drinkField;
        }
        set
        {
            this.free_drinkField = value;
        }
    }

    /// <remarks/>
    public string free_food
    {
        get
        {
            return this.free_foodField;
        }
        set
        {
            this.free_foodField = value;
        }
    }

    /// <remarks/>
    public string private_room
    {
        get
        {
            return this.private_roomField;
        }
        set
        {
            this.private_roomField = value;
        }
    }

    /// <remarks/>
    public string horigotatsu
    {
        get
        {
            return this.horigotatsuField;
        }
        set
        {
            this.horigotatsuField = value;
        }
    }

    /// <remarks/>
    public string tatami
    {
        get
        {
            return this.tatamiField;
        }
        set
        {
            this.tatamiField = value;
        }
    }

    /// <remarks/>
    public string card
    {
        get
        {
            return this.cardField;
        }
        set
        {
            this.cardField = value;
        }
    }

    /// <remarks/>
    public string non_smoking
    {
        get
        {
            return this.non_smokingField;
        }
        set
        {
            this.non_smokingField = value;
        }
    }

    /// <remarks/>
    public string charter
    {
        get
        {
            return this.charterField;
        }
        set
        {
            this.charterField = value;
        }
    }

    /// <remarks/>
    public string parking
    {
        get
        {
            return this.parkingField;
        }
        set
        {
            this.parkingField = value;
        }
    }

    /// <remarks/>
    public string barrier_free
    {
        get
        {
            return this.barrier_freeField;
        }
        set
        {
            this.barrier_freeField = value;
        }
    }

    /// <remarks/>
    public string show
    {
        get
        {
            return this.showField;
        }
        set
        {
            this.showField = value;
        }
    }

    /// <remarks/>
    public string karaoke
    {
        get
        {
            return this.karaokeField;
        }
        set
        {
            this.karaokeField = value;
        }
    }

    /// <remarks/>
    public string band
    {
        get
        {
            return this.bandField;
        }
        set
        {
            this.bandField = value;
        }
    }

    /// <remarks/>
    public string tv
    {
        get
        {
            return this.tvField;
        }
        set
        {
            this.tvField = value;
        }
    }

    /// <remarks/>
    public string english
    {
        get
        {
            return this.englishField;
        }
        set
        {
            this.englishField = value;
        }
    }

    /// <remarks/>
    public string pet
    {
        get
        {
            return this.petField;
        }
        set
        {
            this.petField = value;
        }
    }

    /// <remarks/>
    public string child
    {
        get
        {
            return this.childField;
        }
        set
        {
            this.childField = value;
        }
    }

    /// <remarks/>
    public resultsShopCoupon_urls coupon_urls
    {
        get
        {
            return this.coupon_urlsField;
        }
        set
        {
            this.coupon_urlsField = value;
        }
    }

    /// <remarks/>
    public string course
    {
        get
        {
            return this.courseField;
        }
        set
        {
            this.courseField = value;
        }
    }

    /// <remarks/>
    public resultsShopPhoto photo
    {
        get
        {
            return this.photoField;
        }
        set
        {
            this.photoField = value;
        }
    }

    /// <remarks/>
    public string lunch
    {
        get
        {
            return this.lunchField;
        }
        set
        {
            this.lunchField = value;
        }
    }

    /// <remarks/>
    public string midnight
    {
        get
        {
            return this.midnightField;
        }
        set
        {
            this.midnightField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://webservice.recruit.co.jp/HotPepper/")]
public partial class resultsShopLarge_service_area
{

    private string codeField;

    private string nameField;

    /// <remarks/>
    public string code
    {
        get
        {
            return this.codeField;
        }
        set
        {
            this.codeField = value;
        }
    }

    /// <remarks/>
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://webservice.recruit.co.jp/HotPepper/")]
public partial class resultsShopService_area
{

    private string codeField;

    private string nameField;

    /// <remarks/>
    public string code
    {
        get
        {
            return this.codeField;
        }
        set
        {
            this.codeField = value;
        }
    }

    /// <remarks/>
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://webservice.recruit.co.jp/HotPepper/")]
public partial class resultsShopLarge_area
{

    private string codeField;

    private string nameField;

    /// <remarks/>
    public string code
    {
        get
        {
            return this.codeField;
        }
        set
        {
            this.codeField = value;
        }
    }

    /// <remarks/>
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://webservice.recruit.co.jp/HotPepper/")]
public partial class resultsShopMiddle_area
{

    private string codeField;

    private string nameField;

    /// <remarks/>
    public string code
    {
        get
        {
            return this.codeField;
        }
        set
        {
            this.codeField = value;
        }
    }

    /// <remarks/>
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://webservice.recruit.co.jp/HotPepper/")]
public partial class resultsShopSmall_area
{

    private string codeField;

    private string nameField;

    /// <remarks/>
    public string code
    {
        get
        {
            return this.codeField;
        }
        set
        {
            this.codeField = value;
        }
    }

    /// <remarks/>
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://webservice.recruit.co.jp/HotPepper/")]
public partial class resultsShopGenre
{

    private string codeField;

    private string nameField;

    private string catchField;

    /// <remarks/>
    public string code
    {
        get
        {
            return this.codeField;
        }
        set
        {
            this.codeField = value;
        }
    }

    /// <remarks/>
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    public string @catch
    {
        get
        {
            return this.catchField;
        }
        set
        {
            this.catchField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://webservice.recruit.co.jp/HotPepper/")]
public partial class resultsShopBudget
{

    private string codeField;

    private string nameField;

    private string averageField;

    /// <remarks/>
    public string code
    {
        get
        {
            return this.codeField;
        }
        set
        {
            this.codeField = value;
        }
    }

    /// <remarks/>
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    public string average
    {
        get
        {
            return this.averageField;
        }
        set
        {
            this.averageField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://webservice.recruit.co.jp/HotPepper/")]
public partial class resultsShopUrls
{

    private string pcField;

    /// <remarks/>
    public string pc
    {
        get
        {
            return this.pcField;
        }
        set
        {
            this.pcField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://webservice.recruit.co.jp/HotPepper/")]
public partial class resultsShopCoupon_urls
{

    private string pcField;

    private string spField;

    /// <remarks/>
    public string pc
    {
        get
        {
            return this.pcField;
        }
        set
        {
            this.pcField = value;
        }
    }

    /// <remarks/>
    public string sp
    {
        get
        {
            return this.spField;
        }
        set
        {
            this.spField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://webservice.recruit.co.jp/HotPepper/")]
public partial class resultsShopPhoto
{

    private resultsShopPhotoPC pcField;

    private resultsShopPhotoMobile mobileField;

    /// <remarks/>
    public resultsShopPhotoPC pc
    {
        get
        {
            return this.pcField;
        }
        set
        {
            this.pcField = value;
        }
    }

    /// <remarks/>
    public resultsShopPhotoMobile mobile
    {
        get
        {
            return this.mobileField;
        }
        set
        {
            this.mobileField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://webservice.recruit.co.jp/HotPepper/")]
public partial class resultsShopPhotoPC
{

    private string lField;

    private string mField;

    private string sField;

    /// <remarks/>
    public string l
    {
        get
        {
            return this.lField;
        }
        set
        {
            this.lField = value;
        }
    }

    /// <remarks/>
    public string m
    {
        get
        {
            return this.mField;
        }
        set
        {
            this.mField = value;
        }
    }

    /// <remarks/>
    public string s
    {
        get
        {
            return this.sField;
        }
        set
        {
            this.sField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://webservice.recruit.co.jp/HotPepper/")]
public partial class resultsShopPhotoMobile
{

    private string lField;

    private string sField;

    /// <remarks/>
    public string l
    {
        get
        {
            return this.lField;
        }
        set
        {
            this.lField = value;
        }
    }

    /// <remarks/>
    public string s
    {
        get
        {
            return this.sField;
        }
        set
        {
            this.sField = value;
        }
    }
}

