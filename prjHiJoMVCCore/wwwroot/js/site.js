// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// 全域共用Events刪除函式
window.confirmDelete = async function (id, name, onSuccess = null) {
    const result = await Swal.fire({
        title: '確定要刪除？',
        text: `「${name}」將被永久刪除！`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#aaa',
        confirmButtonText: '是，刪除',
        cancelButtonText: '取消'
    });

    if (!result.isConfirmed) return;

    try {
        const response = await fetch('/ApiEvents/Delete', {
            method: 'POST',
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            body: new URLSearchParams({ id })
        });

        if (response.ok) {
            await Swal.fire({
                icon: 'success',
                title: '刪除成功',
                showConfirmButton: false,
                timer: 1000
            });

            // 刪除成功後做指定行為
            if (typeof onSuccess === 'function') {
                onSuccess();
            } else {
                // 預設：導回列表頁
                window.location.href = '/Events/List';
            }
        } else {
            const errText = await response.text();
            Swal.fire('刪除失敗', errText, 'error');
        }
    } catch (err) {
        Swal.fire('錯誤', '發生例外錯誤：' + err.message, 'error');
    }
};

function deletePhoto(photoId) {
    Swal.fire({
        title: "確定要刪除這張圖片嗎？",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "刪除",
        cancelButtonText: "取消"
    }).then(async (result) => {
        if (result.isConfirmed) {
            const response = await fetch(`/ApiEvents/DeletePhoto`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded"
                },
                body: new URLSearchParams({ id: photoId })
            });

            if (response.ok) {
                location.reload(); // 或 AJAX 更新畫面
            } else {
                const msg = await response.text();
                Swal.fire("刪除失敗", msg, "error");
            }
        }
    });
}

// ✅ 封面刪除功能（Create & Edit 通用）
window.removeCoverImage = function () {
    console.log("removeCoverImage 被呼叫");

    const img = document.getElementById("coverImgPreview");
    if (img) img.src = "/images/placeholder.png";  // 還原為預設圖

    const btn = document.getElementById("coverRemoveBtn");
    if (btn) btn.classList.add("d-none");  // 隱藏 X

    const fileInput = document.getElementById("CoverPhoto");
    if (fileInput) fileInput.value = "";  // 清空 input file

    const deletedFlag = document.getElementById("CoverPhotoDeleted");
    if (deletedFlag) deletedFlag.value = "true";  // 設定刪除意圖
};

// ✅ 圖片預覽功能（Create & Edit 通用）
window.previewImage = function (input, targetId = "coverImgPreview", isCover = false) {
    const file = input.files?.[0];
    if (!file) return;

    const preview = document.getElementById(targetId);
    if (!preview) return;

    const reader = new FileReader();
    reader.onload = e => {
        preview.src = e.target.result;
    };
    reader.readAsDataURL(file);

    if (isCover) {
        const removeBtn = document.getElementById("coverRemoveBtn");
        if (removeBtn) removeBtn.classList.remove("d-none");

        const deletedFlag = document.getElementById("CoverPhotoDeleted");
        if (deletedFlag) deletedFlag.value = "false";  // 使用者重新補上封面
    }
};








