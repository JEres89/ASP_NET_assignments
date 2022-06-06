let controller = document.querySelector("title").textContent;

function GetCreate() {
	$.ajax(
		{
			type: 'GET',
			url: `/${controller}/Create`,
			success: function (response) {
				$("#create_view").html(response);
			}
		});
}

function GetDetails() {
	let id = parseInt($("#id_text").val());
	if (!id || id < 0) {
		return;
	}
	$.ajax(
		{
			type: 'GET',
			url: `/${controller}/Details/${id}`,
			success: function (response) {
				$("#details_view").html(response);
			},
			error: function (response) {
				$("#details_view").html(response.responseJSON);
			}
		}
	);
}
function ToggleDetails() {
	$("#details_container").toggle("fast");
	//css("display", "block");
}
function ShowDetails() {
	$("#details_container").show("fast");
}
function ShowList() {
	//$("#details_container").css("display", "none");
	$("#list_container").toggle("fast");
}
function ShowCreate() {
	$("#create_view").toggle("fast");
}
$(document).ready(function () {

	$("#show_list").on("click", ShowList);
	$("#show_details").on("click", ToggleDetails);
	$("#new_entry").on("click", function () {
		let view = $("#create_view");

		if (view.children("form").length === 1) {
			ShowCreate();
		}
		else {
			GetCreate();
			ShowCreate();
		}
	});

	$("#get_details").on("click", GetDetails);

	$("#search_entries").on("submit", function (ev) {
		ev.preventDefault();
		let searchValue = $("#search_value").val();
		$.ajax(
			{
				type: 'POST',
				url: `/${controller}/Search`,
				data: { searchValue : searchValue },
				success: function (response) {
					$("#list_view").html(response);
				},
				error: function (response) {
					$("#message").html(response);
				}
			});
	});

	$("#remove_row").on("click", function () {
		let id = parseInt($("#id_text").val());
		$.ajax(
			{
				type: 'POST',
				url: `/${controller}/Delete/${id}`,
				success: function (response) {
					$(`#${id}`).remove();
					$("#details_view").html(response);
				},
				error: function (response) {

					$("#details_view").html(response.responseJSON);
				}
			});
	});

	$(`#${controller}_view`).on("click", "[name='delete_row']", function () {
		let row = $(this).closest(".row");

		let id = parseInt(row.prop("id"));
		$.ajax(
			{
				type: 'POST',
				url: `/${controller}/Delete/${id}`,
				success: function (response) {
					if (row.parent().attr("id") == "details_view") {
						$(`#${id}`).remove();
						$("#details_view").html(response);
					} else {
						$("#message").html(response);
						row.remove;
					}
					row.remove();
				},
				error: function () {

				}
			});
	});
	$("#list_view").on("click", ".itemRow", function (ev) {
		let target = $(ev.target);
		if (!target.hasClass("removeEntitybtn")) {
			let entity_id = $(this).closest(".itemRow").prop("id");

			$("#id_text").val(entity_id);
			ShowDetails();
			GetDetails();
		}
	});
});