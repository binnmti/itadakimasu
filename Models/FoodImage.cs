﻿using System.ComponentModel.DataAnnotations;

namespace Models;
public record FoodImage
{
    public long Id { get; set; }

    [MaxLength(100)]
    public string FoodName { get; set; } = "";
    public string BaseUrl { get; set; } = "";
    [MaxLength(10)]
    public string SearchAPI { get; set; } = "";
    [MaxLength(20)]
    public string BlobName { get; set; } = "";
    public int BlobWidth { get; set; }
    public int BlobHeight { get; set; }
    public long BlobSize { get; set; }
    //サムネイル
    [MaxLength(20)]
    public string BlobSName { get; set; } = "";
    public int BlobSWidth { get; set; }
    public int BlobSHeight { get; set; }
    public long BlobSSize { get; set; }

    public int StatusNumber { get; set; }

}
