let controller = document.querySelector("title").textContent;

function ShowCreate() {
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
function ShowDetails() {
	$("#details_container").toggle();
	//css("display", "block");
}
function ShowList() {
	//$("#details_container").css("display", "none");
	$("#list_container").toggle();
}
$(document).ready(function () {

	$("#show_list").on("click", ShowList);
	$("#show_details").on("click", ShowDetails);
	$("#new_entry").on("click", ShowCreate);

	$("#get_details").on("click", GetDetails);

	$("#search_entries").on("submit", function (ev) {
		ev.preventDefault();
		let searchValue = $("#search_value").val();
		$.ajax(
			{
				type: 'POST',
				url: `/${controller}/Search`,
				data: searchValue,
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